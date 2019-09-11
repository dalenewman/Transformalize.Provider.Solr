nuget pack Transformalize.Provider.Solr.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Solr.Autofac.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Solr.Autofac.v3.nuspec -OutputDirectory "c:\temp\modules"


REM nuget push "c:\temp\modules\Transformalize.Provider.Solr.0.6.16-beta.nupkg" -source https://api.nuget.org/v3/index.json
REM nuget push "c:\temp\modules\Transformalize.Provider.Solr.Autofac.0.6.16-beta.nupkg" -source https://api.nuget.org/v3/index.json
REM nuget push "c:\temp\modules\Transformalize.Provider.Solr.Autofac.v3.0.6.16-beta.nupkg" -source https://api.nuget.org/v3/index.json





