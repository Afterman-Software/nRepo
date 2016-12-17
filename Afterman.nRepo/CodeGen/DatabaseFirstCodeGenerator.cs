using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Afterman.nRepo.CodeGen
{
    public class SchemaRow
    {
        public string ColumnName;
        public bool IsNullable;
        public string DataType;
        public int? MaxLength;
        public string TableName;
    }
    public class DatabaseFirstCodeGenerator
    {
        private readonly string _connectionString;

        public DatabaseFirstCodeGenerator(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IDictionary<string,IList<SchemaRow>> GenerateRawClasses()
        {
            var propertiesByClass = new Dictionary<string, IList<SchemaRow>>();
            foreach (var item in this.GetAllColumns())
            {
                if (!propertiesByClass.ContainsKey(item.TableName))
                    propertiesByClass.Add(item.TableName, new List<SchemaRow>());
                propertiesByClass[item.TableName].Add(item);
            }
            return propertiesByClass;
        }

        public IList<string> GenerateAllClasses()
        {
            var results = new List<string>();
            var propertiesByClass = GenerateRawClasses();

            foreach (var item in propertiesByClass)
            {
                var allRowsString = String.Empty;
                foreach (var row in item.Value)
                {
                    allRowsString = allRowsString + "\n" +
                                    String.Format(this.PropertyTemplate, GetFullDataType(row), row.ColumnName);
                }
                var classTemplate = String.Format(this.ClassTemplate, item.Key, allRowsString);
                results.Add(classTemplate);
            }

            return results;
        }
        public IList<string> GenerateAllClassMaps()
        {
            var results = new List<string>();
            var propertiesByClass = new Dictionary<string, IList<SchemaRow>>();
            foreach (var item in this.GetAllColumns())
            {
                if (!propertiesByClass.ContainsKey(item.TableName))
                    propertiesByClass.Add(item.TableName, new List<SchemaRow>());
                propertiesByClass[item.TableName].Add(item);
            }

            foreach (var item in propertiesByClass)
            {
                var allRowsString = String.Empty;
                foreach (var row in item.Value)
                {
                    allRowsString = allRowsString + "\n" +
                                    String.Format(this.PropertyMapTemplate, row.ColumnName,row.ColumnName);
                }
                var classTemplate = String.Format(this.ClassMapTemplate, item.Key, allRowsString);
                results.Add(classTemplate);
            }

            return results;
        }

       

        private string GetDataType(SchemaRow row)
        {
            row.DataType = row.DataType.ToLower();
            if (row.DataType == "varchar")
                return "string";
            if (row.DataType == "bit")
                return "bool";
            if (row.DataType == "datetime")
                return "DateTime";
            if (row.DataType == "decimal")
                return "decimal";
            if (row.DataType == "float")
                return "decimal";
            if (row.DataType == "money")
                return "decimal";
            if (row.DataType == "int")
                return "int";
            if (row.DataType == "bigint")
                return "long";
            return "string";
        }

        private string GetFullDataType(SchemaRow row)
        {
            var raw = GetDataType(row);
            if (raw == "string") return raw;
            return row.IsNullable ? raw + "?" : raw;
        }

        public IList<SchemaRow> GetAllColumns()
        {
            var results = new List<SchemaRow>();
            var sql = @"SELECT
                COLUMN_NAME,IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, TABLE_NAME
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = 'dbo'";
            using (var conn = new SqlConnection(this._connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new SchemaRow()
                        {
                            ColumnName = reader["COLUMN_NAME"].ToString(),
                            DataType = reader["DATA_TYPE"].ToString(),
                            IsNullable = reader["IS_NULLABLE"].ToString() == "YES",
                            MaxLength =
                                Convert.IsDBNull(reader["CHARACTER_MAXIMUM_LENGTH"])
                                    ? default(int?)
                                    : Convert.ToInt32(reader["CHARACTER_MAXIMUM_LENGTH"]),
                            TableName = Convert.ToString(reader["TABLE_NAME"])
                        });
                    }
                }
                conn.Close();
            }
            return results;
        }
        public string ClassTemplate = @"

public class {0}
{{
    {1}
}}

";

        public string ClassMapTemplate = @"

public class {0}Map :
    ClassMap<{0}>
{{
    public {0}Map()
    {{
        {1}
    }}
}}

";

        public string PropertyMapTemplate = "Map(x=> x.{0},\"{1}\");";
        public string PropertyTemplate = @"public virtual {0} {1} {{ get; set; }}";
    }





}
