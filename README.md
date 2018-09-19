### Overview

This adds a `Solr` provider to Transformalize using [SolrNet](https://github.com/SolrNet/SolrNet).  It is a plug-in compatible with Transformalize 0.3.7-beta.  This is tested on SOLR 6.2.1 and SOLR 7.4.0.

Build the Autofac project and put it's output into Transformalize's *plugins* folder.

### Write Usage

```xml
<add name='TestProcess' mode='init'>
  <connections>
    <add name='input' provider='bogus' seed='1' />
    <add name='output' provider='solr' core='bogus' folder='c:\java\solr-6.2.1\cores' path='solr' port='8983' />
  </connections>
  <entities>
    <add name='Contact' size='1000'>
      <fields>
        <add name='FirstName' />
        <add name='LastName' />
        <add name='Stars' type='byte' min='1' max='5' />
        <add name='Reviewers' type='int' min='0' max='500' />
      </fields>
    </add>
  </entities>
</add>
```

This writes 1000 rows of bogus data to a Solr 6 core at *c:\java\solr-6.2.1\cores\bogus*.

### Read Usage

```xml
<add name='TestProcess' >
  <connections>
    <add name='input' provider='solr' core='bogus' folder='c:\java\solr-6.2.1\cores' path='solr' port='8983' />
  </connections>
  <entities>
    <add name='Contact' page='1' size='10'>
      <fields>
        <add name='firstname' />
        <add name='lastname' />
        <add name='stars' type='byte' />
        <add name='reviewers' type='int' />
      </fields>
    </add>
  </entities>
</add>
```

This reads 10 rows of bogus data from a Solr 6 core at *c:\java\solr-6.2.1\cores\bogus*:

<pre>
<strong>firstname,lastname,stars,reviewers</strong>
Justin,Konopelski,3,153
Eula,Schinner,2,41
Tanya,Shanahan,4,412
Emilio,Hand,4,469
Rachel,Abshire,3,341
Doyle,Beatty,4,458
Delbert,Durgan,2,174
Harold,Blanda,4,125
Willie,Heaney,5,342
Sophie,Hand,2,176</pre>

### Notes

- Tested with Solr 6.
- Field names go into Solr as lower case.
- Uses older SolrNet at *https://ci.appveyor.com/nuget/solrnet-022x5w7kmuba*.
