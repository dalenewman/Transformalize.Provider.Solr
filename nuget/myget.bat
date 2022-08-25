nuget pack Transformalize.Provider.Solr.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Solr.Autofac.nuspec -OutputDirectory "c:\temp\modules"

nuget push "c:\temp\modules\Transformalize.Provider.Solr.0.10.5-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json
nuget push "c:\temp\modules\Transformalize.Provider.Solr.Autofac.0.10.5-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json
