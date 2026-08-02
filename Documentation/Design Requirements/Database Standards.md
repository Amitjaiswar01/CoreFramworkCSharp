# Lamps Plus Test Automation Database Standards
This document details the code standards and preferences for development of code which requests information from the Lamps Plus databases.  
Tasks related to work in this area will typically be found under https://lampstrack.lampsplus.com:8443/browse/ACD-5008.  
Database code is located https://bitbucket.lampsplus.com:8443/projects/LAMPS/repos/test-automation/browse/LampsPlus.AutomationFramework/Databases.

## Queries
Queries are SQL strings to request information from the database.  
Queries are located at https://bitbucket.lampsplus.com:8443/projects/LAMPS/repos/test-automation/browse/LampsPlus.AutomationFramework/Databases/Queries

### Required
- Queries are organized in single class files with requests of similar purpose.  
- All queries have a summary explaining the purpose and the important constraints.
- All appropriate tags are added to the summary of the query string.

### Optional
- Any general tags that should be added to the summary should be updated in the Tags section.

#### Tags
- RandomSku
- Listable
