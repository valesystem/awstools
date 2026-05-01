using System.Data;
using System.Reflection;
using System.Text;

namespace apiCirculante.Repository
{


    public class ParseSql
    {

        private static string DoubletoString(double valor)
        {
            string vlrstr = "0";
            try
            {
                vlrstr = valor.ToString();
            }
            catch
            {
                vlrstr = "0";
            }
            return vlrstr.Replace(',', '.');
        }

        public string ObjetoparaSql<T>(T objeto, string nometabela)
        {
            bool inserir = false;
            StringBuilder cmd1 = new StringBuilder();
            StringBuilder cmd2 = new StringBuilder();

            var propriedades = objeto.GetType().GetProperties();
            string nomeChavePrimaria = "id";
            // verificar se tem um propriedade codigo ou id com valor zero , se sim é insert , caso contrario é update
            for (int i = 0; i < propriedades.Length; i++)
            {
                if (propriedades[i].Name.ToLower() == "codigo" || propriedades[i].Name.ToLower() == "id")
                {
                    nomeChavePrimaria = propriedades[i].Name.ToLower();
                    int id = 0;
                    try
                    {
                        id = Convert.ToInt32(propriedades[i].GetValue(objeto));
                    }
                    catch
                    {
                        id = 0;
                    }

                    if (id == 0)
                    {
                        inserir = true;
                        cmd1.Append("(");
                        cmd2.Append("values (");
                    }
                    else
                        cmd2.Append(" where (" + nomeChavePrimaria + "=" + id.ToString() + ") ");
                    break;
                }
            }

            string resultado = "";
            int vezes = 0;

            foreach (var propriedade in propriedades)
            {
                if (propriedade.Name.ToLower() != "codigo" && propriedade.Name.ToLower() != "id" && !propriedade.Name.ToLower().Contains("_"))
                {
                    vezes = vezes + 1;
                    if (vezes > 1)
                    {
                        if (inserir == true)
                        {
                            cmd1.Append(",");
                            cmd2.Append(",");
                        }
                        else
                            cmd1.Append(",");

                    }



                    if (propriedade.PropertyType.Name == "String")
                    {
                        string str = (string)propriedade.GetValue(objeto);
                        if (str != null)
                        {
                            if (str.ToString() != "")
                                str = str.Replace("'", " ");

                        }
                        if (inserir == true)
                        {
                            cmd1.Append(propriedade.Name);
                            cmd2.Append(String.Format("'{0}'", str));
                        }
                        else
                        {
                            cmd1.Append(String.Format("{0}='{1}'", propriedade.Name, propriedade.GetValue(objeto)));
                        }
                    }
                    if (propriedade.PropertyType.Name == "Int32")
                    {

                        if (inserir == true)
                        {
                            cmd1.Append(propriedade.Name);
                            cmd2.Append(String.Format("'{0}'", propriedade.GetValue(objeto)));
                        }
                        else
                        {
                            cmd1.Append(String.Format("{0}={1}", propriedade.Name, propriedade.GetValue(objeto)));
                        }

                    }

                    if (propriedade.PropertyType.Name == "DateTime")
                    {
                        string data = Convert.ToString(propriedade.GetValue(objeto));
                        try
                        {
                            data = DateTime.Parse(data).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        catch
                        {
                            data = "";
                        }

                        if (inserir == true)
                        {
                            cmd1.Append(propriedade.Name);
                            cmd2.Append(String.Format("'{0}'", data));
                        }
                        else
                        {
                            cmd1.Append(String.Format("{0}='{1}'", propriedade.Name, data));
                        }

                    }

                    if ((propriedade.PropertyType.Name == "Double") || (propriedade.PropertyType.Name == "Single"))
                    {

                        string valor = DoubletoString(Convert.ToDouble(propriedade.GetValue(objeto)));
                        if (inserir == true)
                        {
                            cmd1.Append(propriedade.Name);
                            cmd2.Append(String.Format("'{0}'", valor));
                        }
                        else
                        {
                            cmd1.Append(String.Format("{0}='{1}'", propriedade.Name, valor));
                        }

                    }


                    if (propriedade.PropertyType.Name == "TimeSpan")
                    {

                        if (inserir == true)
                        {
                            cmd1.Append(propriedade.Name);
                            cmd2.Append(String.Format("'{0}'", propriedade.GetValue(objeto)));
                        }
                        else
                        {
                            cmd1.Append(String.Format("{0}='{1}'", propriedade.Name, propriedade.GetValue(objeto)));
                        }

                    }


                }
            }

            if (inserir == true)
            {
                cmd1.Append(")");
                cmd2.Append(")");
                resultado = string.Format("INSERT INTO {0} ", nometabela) + cmd1.ToString() + " " + cmd2.ToString();
            }
            else
            {
                resultado = string.Format("UPDATE {0} SET ", nometabela) + cmd1.ToString() + " " + cmd2;
            }
            return resultado;
        }

        public List<T> DataTabletoList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (dr[column.ColumnName].GetType() == typeof(System.DBNull))
                        {
                            if (column.DataType == System.Type.GetType("System.Int32"))
                            {
                                pro.SetValue(obj, 0, null);
                            }
                            if (column.DataType == System.Type.GetType("System.String"))
                            {
                                pro.SetValue(obj, "", null);
                            }
                        }

                        else
                        {
                            try
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            }
                            catch
                            {
                                if (dr[column.ColumnName].GetType().FullName == "System.TimeSpan" || dr[column.ColumnName].GetType().FullName == "System.DateTime")
                                    pro.SetValue(obj, 0, null);
                                else
                                    pro.SetValue(obj, "", null);
                            }
                        }
                    }


                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
