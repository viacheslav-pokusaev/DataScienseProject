@echo off
setlocal EnableDelayedExpansion

if [%1] == [] goto :ShowHelp
if [%2] == [] goto :ShowHelp
if [%3] == [] goto :ShowHelp
if [%4] == [] goto :ShowHelp
if [%5] == [] goto :ShowHelp

set server=%2
set db=%3.dbo.
set user=%4
set pass=%5

if [%1] == [in] goto :import
if [%1] == [out] goto :export
goto :ShowHelp

:import
set /P confirm=Enter "yes" to load data into %db% on %server%: 
if [%confirm%] NEQ [yes] goto :end
echo Import is beginning
BCP %db%ViewTypes in ViewTypes.txt -c -S %server% -U %user% -P %pass%
BCP %db%Views in Views.txt -c -S %server% -U %user% -P %pass%
BCP %db%ElementTypes in ElementTypes.txt -c -S %server% -U %user% -P %pass%
BCP %db%LinkTypes in LinkTypes.txt -c -S %server% -U %user% -P %pass%
BCP %db%Elements in Elements.txt -c -S %server% -U %user% -P %pass%
BCP %db%ElementParameters in ElementParameters.txt -c -S %server% -U %user% -P %pass%
BCP %db%ViewElements in ViewElements.txt -c -S %server% -U %user% -P %pass%
BCP %db%Directions in Directions.txt -c -S %server% -U %user% -P %pass%
BCP %db%Tags in Tags.txt -c -S %server% -U %user% -P %pass%
BCP %db%ViewTags in ViewTags.txt -c -S %server% -U %user% -P %pass%
BCP %db%Executors in Executors.txt -c -S %server% -U %user% -P %pass%
BCP %db%ExecutorRoles in ExecutorRoles.txt -c -S %server% -U %user% -P %pass%
BCP %db%ViewExecutors in ViewExecutors.txt -c -S %server% -U %user% -P %pass%
BCP %db%Groups in Groups.txt -c -S %server% -U %user% -P %pass%
BCP %db%GroupViews in GroupViews.txt -c -S %server% -U %user% -P %pass%
BCP %db%Passwords in Passwords.txt -c -S %server% -U %user% -P %pass%
BCP %db%VisitLogs in VisitLogs.txt -c -S %server% -U %user% -P %pass%
BCP %db%VisitViews in VisitViews.txt -c -S %server% -U %user% -P %pass%
BCP %db%Feedback in Feedback.txt -c -S %server% -U %user% -P %pass%
BCP %db%ConfigValues in ConfigValues.txt -c -S %server% -U %user% -P %pass%
echo Import has done
goto :end

:export
echo Export is beginning
BCP %db%ViewTypes out ViewTypes.txt -c -S %server% -U %user% -P %pass%
BCP %db%Views out Views.txt -c -S %server% -U %user% -P %pass%
BCP %db%ElementTypes out ElementTypes.txt -c -S %server% -U %user% -P %pass%
BCP %db%LinkTypes out LinkTypes.txt -c -S %server% -U %user% -P %pass%
BCP %db%Elements out Elements.txt -c -S %server% -U %user% -P %pass%
BCP %db%ElementParameters out ElementParameters.txt -c -S %server% -U %user% -P %pass%
BCP %db%ViewElements out ViewElements.txt -c -S %server% -U %user% -P %pass%
BCP %db%Directions out Directions.txt -c -S %server% -U %user% -P %pass%
BCP %db%Tags out Tags.txt -c -S %server% -U %user% -P %pass%
BCP %db%ViewTags out ViewTags.txt -c -S %server% -U %user% -P %pass%
BCP %db%Executors out Executors.txt -c -S %server% -U %user% -P %pass%
BCP %db%ExecutorRoles out ExecutorRoles.txt -c -S %server% -U %user% -P %pass%
BCP %db%ViewExecutors out ViewExecutors.txt -c -S %server% -U %user% -P %pass%
BCP %db%Groups out Groups.txt -c -S %server% -U %user% -P %pass%
BCP %db%GroupViews out GroupViews.txt -c -S %server% -U %user% -P %pass%
BCP %db%Passwords out Passwords.txt -c -S %server% -U %user% -P %pass%
BCP %db%VisitLogs out VisitLogs.txt -c -S %server% -U %user% -P %pass%
BCP %db%VisitViews out VisitViews.txt -c -S %server% -U %user% -P %pass%
BCP %db%Feedback out Feedback.txt -c -S %server% -U %user% -P %pass%
BCP %db%ConfigValues out ConfigValues.txt -c -S %server% -U %user% -P %pass%
echo Export has done
goto :end

:ShowHelp
echo This script will export portfolio data to files or import to DB
echo USAGE:
echo dbtransfer.cmd out server_name db_name user password
echo dbtransfer.cmd in server_name db_name user password
echo dbtransfer.cmd out server_name CS_DS_Portfolio user password

:end
echo exit