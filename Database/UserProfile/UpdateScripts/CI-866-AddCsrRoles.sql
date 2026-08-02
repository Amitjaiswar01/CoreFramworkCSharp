-- =============================================
-- Author: John Hilts
-- Create date: 01/02/2020
-- Description: CI-866 : Add Required Group Roles to CSR logins used by Test Automation
-- =============================================

use UserProfile
go

declare @ExistingTestCsrRoles table (RoleGroupId uniqueidentifier, UserTypeId int)
insert into @ExistingTestCsrRoles
select rg.Id, case when u.UserName = 'autocsrregular@lampsplus.com' then 4 else 5 end UserTypeId
  from aspnet_Users (nolock) u
  inner join tblUserProfile (nolock) up on u.UserId = up.UserId
  left join tblUsersInRoleGroups (nolock) g on u.UserId = g.UserId
  left join tblRoleGroups (nolock) rg on g.RoleGroupId = rg.Id
  where u.UserName in ('autocsrregular@lampsplus.com', 'autocsrmanager@lampsplus.com')

insert into tblUsersInRoleGroups (UserId, RoleGroupId)
select u.UserId, e.RoleGroupId
  from aspnet_Users (nolock) u
  inner join tblUserProfile (nolock) up on u.UserId = up.UserId
  inner join tblAutomationAccount (nolock) aa on u.UserName = aa.UserName and aa.UserTypeId in (4, 5)
  inner join @ExistingTestCsrRoles e on aa.UserTypeId = e.UserTypeId
where not exists(select 1 from tblUsersInRoleGroups (nolock) rg where rg.UserId = u.UserId and rg.RoleGroupId = e.RoleGroupId)
order by 1, 2
