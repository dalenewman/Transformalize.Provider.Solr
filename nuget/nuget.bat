nuget pack Transformalize.Provider.Solr.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Solr.Autofac.nuspec -OutputDirectory "c:\temp\modules"

nuget push "c:\temp\modules\Transformalize.Provider.Solr.0.3.8-beta.nupkg" -source https://api.nuget.org/v3/index.json
nuget push "c:\temp\modules\Transformalize.Provider.Solr.Autofac.0.3.8-beta.nupkg" -source https://api.nuget.org/v3/index.json






