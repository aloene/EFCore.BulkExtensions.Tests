using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EFCore.BulkExtensions.Tests
{
    public class BulkInsertTest
    {
        private ITestOutputHelper _output;

        public BulkInsertTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task BulkInsertIsFastEnough()
        {
            var sw = Stopwatch.StartNew();

            using (var dbContext = new Entities.TestDatabaseContext())
            {
                using (var dbTransaction = await dbContext.Database.BeginTransactionAsync())
                {
                    _output.WriteLine(sw.Elapsed.Seconds.ToString() +"s - Reading activities...");

                    // Removing this query solves the issue !!
                    var activitiesIds = await dbContext
                        .Activities
                        .ToDictionaryAsync(a => int.Parse(a.PublicId, CultureInfo.InvariantCulture), a => new { a.Id, a.ClosedOn });

                    _output.WriteLine(sw.Elapsed.Seconds.ToString() + "s - Activity read.");

                    try
                    {
                        var dbActivities = Enumerable.Range(1, 1000).Select(i =>
                        {
                            return new Entities.Activity
                            {
                                Id = default(int),
                                PublicId = i.ToString(),
                                ActivityTypeId = i % 2 == 0 ? Entities.ActivityTypeEnum.Project : Entities.ActivityTypeEnum.Off,
                                ApplicationId = i % 4 == 0 ? (int?)null : i % 4,
                                UpdatedOn = DateTime.Now,
                                ClosedOn = i % 2 == 0 ? (DateTime?)null : DateTime.Now,
                                Name = "Activity " + i.ToString(),
                                DeletedOn = null
                            };
                        }).ToList();

                        _output.WriteLine(sw.Elapsed.Seconds.ToString() + "s - Inserting/Updating activities...");
                        dbContext.BulkInsertOrUpdate(
                            dbActivities,
                            new BulkConfig
                            {
                                BulkCopyTimeout = 0,
                                SetOutputIdentity = true,
                                UpdateByProperties = new List<string> { nameof(Entities.Activity.PublicId), nameof(Entities.Activity.ActivityTypeId) },
                                PropertiesToExclude = new List<string> { nameof(Entities.Activity.Id), nameof(Entities.Activity.ParentId) }
                            });
                        _output.WriteLine(sw.Elapsed.Seconds.ToString() + "s - Activities written.");

                        dbTransaction.Commit();
                        _output.WriteLine(sw.Elapsed.Seconds.ToString() + "s - Transaction committed.");
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }

                sw.Stop();
                Assert.True(sw.Elapsed < new TimeSpan(0, 0, 10));
            }
        }
    }
}
