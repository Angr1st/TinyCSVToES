﻿using Elasticsearch.Net;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace CSVToESLib
{
    class ElasticsearchClient
    {
        Elasticsearch.Net.ElasticLowLevelClient ElasticLowLevelClient = new Elasticsearch.Net.ElasticLowLevelClient();

        public async Task<StringResponse> BulkInsert(ParallelQuery<CsvMappingResult<Person>> results)
        {
            return await ElasticLowLevelClient.BulkAsync<StringResponse>(PostData.MultiJson(results.Where(result => result.IsValid).Select(result => new Object[] { new Index(result.Result, "1") }).Aggregate((newValue, oldValue) => oldValue.Concat(newValue).ToArray())));
        }



    }

    class Index
    {
        public InnerIndex InnerIndexField;
        public Person Person;

        public Index(Person person, string id)
        {
            Person = person;
            InnerIndexField = new InnerIndex(id: id);
        }
    }

    class InnerIndex
    {
        public string _index;
        public string _type;
        public string _id;

        public InnerIndex(string index = "persons", string type = "person", string id = "1")
        {
            _index = index;
            _type = type;
            _id = id;
        }
    }
}
