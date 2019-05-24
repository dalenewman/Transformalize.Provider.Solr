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
using System.Collections.Generic;
using System.Linq;
// using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SolrNet;
using Transformalize.Configuration;
using Transformalize.Context;
using Transformalize.Contracts;
using Transformalize.Extensions;

namespace Transformalize.Providers.Solr {

   public class ParallelSolrWriter : IWrite {

      private readonly OutputContext _context;
      readonly ISolrOperations<Dictionary<string, object>> _solr;
      private readonly Field[] _fields;
      private int _fullCount;
      // private int _originalConnectionLimit;

      public ParallelSolrWriter(OutputContext context, ISolrOperations<Dictionary<string, object>> solr) {
         _context = context;
         _solr = solr;
         _fields = context.OutputFields.Where(f => f.Type != "byte[]").ToArray();
      }

      public void Write(IEnumerable<IRow> rows) {

         // _originalConnectionLimit = ServicePointManager.DefaultConnectionLimit;
         // ServicePointManager.DefaultConnectionLimit = 255;

         Parallel.ForEach(rows.Partition(_context.Entity.InsertSize), part => {
            var batchCount = (uint)0;
            var docs = new List<Dictionary<string, object>>();
            foreach (var row in part) {
               batchCount++;
               Interlocked.Increment(ref _fullCount);
               docs.Add(_fields.ToDictionary(field => field.Alias.ToLower(), field => row[field]));
            }
            var response = _solr.AddRange(docs);
            if (response.Status == 0) {
               var count = batchCount;
               _context.Debug(() => $"{count} to output");
            } else {
               _context.Error($"Couldn't add range of {docs.Count} document{docs.Count.Plural()} to SOLR.");
            }
         });

         _solr.Commit();

         // ServicePointManager.DefaultConnectionLimit = _originalConnectionLimit;

         if (_fullCount <= 0)
            return;

         _context.Entity.Inserts += System.Convert.ToUInt32(_fullCount);
         _context.Info($"{_fullCount} to output");
      }
   }
}