-- =============================================
-- Author: John Hilts
-- Create date: 01/24/2020
-- Description: CI-920 : Remove Test CSRs who are not in DOM yet from the available TAF user pool 
-- =============================================

use UserProfile
go

update UserProfile.dbo.tblAutomationAccount 
set UserTypeId = UserTypeId * -1
-- regular and manager CSRs only
where UserTypeId in (4, 5) 
-- exclude users already in DOM
and UserName not in (
'aautocsrregular1@lampsplus.com',
'aautocsrregular3@lampsplus.com',
'aautocsrregular4@lampsplus.com',
'aautocsrregular5@lampsplus.com',
'aautocsrregular6@lampsplus.com',
'aautocsrmanager1@lampsplus.com',
'aautocsrmanager3@lampsplus.com',
'aautocsrmanager4@lampsplus.com',
'aautocsrmanager5@lampsplus.com',
'aautocsrmanager6@lampsplus.com'
)

