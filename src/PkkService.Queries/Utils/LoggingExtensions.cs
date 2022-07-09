
namespace PkkService.Queries.Utils
{
    public static class LoggingExtensions
    {
        /// <summary>
        /// Возвращает подробное описание для ошибки, включая внутренние исключения
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="includeInnerException"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static string ToLogMessage(this Exception ex, bool includeInnerException = true, int depth = 1)
        {
            var sb = new StringBuilder();
            return sb.AppendLine().AppendException(ex, includeInnerException, depth).ToString();
        }

        /// <summary> 
        /// Добавляет полную информацию об исключении к StringBuilder
        /// </summary>
        public static StringBuilder AppendException(this StringBuilder sb, Exception ex, bool includeInnerException = true, int depth = 1)
        {
            sb.AppendExceptionDetails(ex, depth);
            if (!includeInnerException)
                return sb;

            var innerExs = GetComplexInnerExceptions(ex);
            if (innerExs != null)
            {
                sb.AppendTab(depth).AppendFormat("{0}:{1}", innerExs.PropertyName, Environment.NewLine);
                for (var i = 0; i < innerExs.InnerExceptions.Count; i++)
                    sb.AppendTab(depth).AppendFormat("[{0}]------------------------------------", i).AppendLine()
                        .AppendException(innerExs.InnerExceptions[i], true, depth + 1);
            }

            if (ex.InnerException != null)
                sb.AppendTab(depth)
                    .AppendFormat("Inner Exception:{0}", Environment.NewLine)
                    .AppendException(ex.InnerException, true, depth + 1);
            return sb;
        }

        /// <summary> 
        /// Метод для построения базовой информации об исключении
        /// </summary>
        // ReSharper disable once UnusedMethodReturnValue.Local
        private static StringBuilder AppendExceptionDetails(this StringBuilder sb, Exception ex, int depth)
        {
            var props = ex.GetType()
                .GetProperties()
                .Where(x => x.Name != "StackTrace" && x.Name != "InnerException")
                .ToDictionary(x => x.Name, x => x.GetValue(ex, null));
            foreach (var prop in props.Where(x => x.Value != null))
                sb.AppendTab(depth).Append(prop.Key).Append(" = ").AppendValue(prop.Value, depth + 1).AppendLine();
            if (ex.StackTrace != null)
                sb.AppendTab(depth)
                    .AppendLine("StackTrace:")
                    .AppendTab(depth)
                    .AppendValue(ex.StackTrace, depth)
                    .AppendLine();
            return sb;
        }

        private static StringBuilder AppendTab(this StringBuilder sb, int i)
        {
            return sb.Append('\t', i);
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private static StringBuilder AppendValue(this StringBuilder sb, object value, int depth)
        {
            if (value == null)
                return sb;

            return sb.Append(value.ToString().Replace(Environment.NewLine, Environment.NewLine + new string('\t', depth)));
        }

        /// <summary>
        /// Специальный метод для возвращения вложенных исключений для сложных Exception
        /// </summary>
        /// <returns></returns>
        private static ComplexInnerExceptionDetails GetComplexInnerExceptions(Exception ex)
        {
            if (ex is AggregateException aggregate)
                return new ComplexInnerExceptionDetails("InnerExceptions", aggregate.InnerExceptions);

            if (ex is ReflectionTypeLoadException reflectionTypeLoad)
                return new ComplexInnerExceptionDetails("LoaderExceptions", reflectionTypeLoad.LoaderExceptions);

            return null;
        }

        private class ComplexInnerExceptionDetails
        {
            public string PropertyName { get; }
            public IList<Exception> InnerExceptions { get; }

            public ComplexInnerExceptionDetails(string name, IList<Exception> innerExceptions)
            {
                PropertyName = name;
                InnerExceptions = innerExceptions;
            }
        }
    }
}
