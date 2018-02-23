﻿#region license
// Transformalize
// Configurable Extract, Transform, and Load
// Copyright 2013-2017 Dale Newman
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion
using System;
using System.Collections.Generic;
using SolrNet;
using SolrNet.Commands.Parameters;
using Transformalize.Configuration;
using Transformalize.Context;
using Transformalize.Contracts;

namespace Transformalize.Providers.Solr {
    public class SolrInputProvider : IInputProvider {

        private readonly InputContext _context;
        private readonly ISolrReadOnlyOperations<Dictionary<string, object>> _solr;

        public SolrInputProvider(InputContext context, ISolrReadOnlyOperations<Dictionary<string, object>> solr) {
            _context = context;
            _solr = solr;
        }

        public object GetMaxVersion() {
            if (string.IsNullOrEmpty(_context.Entity.Version))
                return null;

            var version = _context.Entity.GetVersionField();

            _context.Debug(() => $"Detecting Max Input Version: {_context.Connection.Database}.{version.Alias.ToLower()}.");

            var result = _solr.Query(
                SolrQuery.All,
                new QueryOptions {
                    StartOrCursor = new StartOrCursor.Start(0),
                    Rows = 1,
                    Fields = new List<string> { version.Name },
                    OrderBy = new List<SortOrder> { new SortOrder(version.Name, SolrNet.Order.DESC) }
                });

            var value = result.NumFound > 0 ? result[0][version.Name] : null;
            if (value != null && value.GetType() != Constants.TypeSystem()[version.Type]) {
                value = version.Convert(value);
            }
            _context.Debug(() => $"Found value: {value ?? "null"}");
            return value;
        }

        public Schema GetSchema(Entity entity = null) {
            throw new NotImplementedException();
        }

        public IEnumerable<IRow> Read() {
            throw new NotImplementedException();
        }
    }
}
