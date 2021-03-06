﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SimpleDB.core.TableManagement;

namespace SimpleDB.core
{
    public static class xDocToColumn
    {
        public static Column tryParse(XElement EntityElement, Table table)
        {
            string columnName = EntityElement.Attribute("Name").Value;
            DataType dataType;
            List<DataOptions> dataOptions = new List<DataOptions>();
            Enum.TryParse<DataType>(EntityElement.Attribute("DataType").Value,out dataType);

            foreach(DataOptions dataOption in Enum.GetValues(typeof(DataOptions)))
            {
                string strOption = dataOption.ToString();
                if (EntityElement.Attribute(strOption) != null)
                {
                    DataOptions tmpDataOption;
                    Enum.TryParse<DataOptions>(strOption,out tmpDataOption);
                    dataOptions.Add(tmpDataOption);

                }
            }

            Column col = new Column(columnName, dataType, dataOptions.ToArray(), EntityElement, table);

            if (EntityElement.Attribute("Optional") != null) col.setOptional(EntityElement.Attribute("Optional").Value);

            //Column column = new Column();
            return col;
        }
    }
}
