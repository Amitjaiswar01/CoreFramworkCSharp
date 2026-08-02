-- =============================================
-- Author: John Hilts
-- Create date: 01/24/2020
-- Description: CI-920 : Rollback script to CI-920-Remove-CSRs.sql
-- =============================================

use UserProfile
go

-- restore any users removed from the available pool
update UserProfile.dbo.tblAutomationAccount 
set UserTypeId = UserTypeId * -1
where UserTypeId < 0

