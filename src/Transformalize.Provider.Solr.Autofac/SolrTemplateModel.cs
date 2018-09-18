using System.Dynamic;
using Transformalize.Configuration;

namespace Transformalize.Providers.Solr.Autofac {
    public class SolrTemplateModel {
        public Process Process { get; set; }
        public ExpandoObject Parameters { get; set; }
    }
}