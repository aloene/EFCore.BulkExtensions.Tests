# EFCore.BulkExtensions.Tests
EFCore.BulkExtensions performance test for issue #148.

Test project showing performance issues with EFCore.BulkExtensions when
- many entities are tracked by EF
- value conversion exsist on the entity type
and before issuing a bulk insert statement.
