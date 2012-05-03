using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Motore.Library.Aws.Attributes;

namespace Motore.Library.Aws.SimpleDb
{
    public class SimpleDbEntityHelper
    {
        public virtual string GetPrimaryKeyValueOfEntity<T>(ISimpleDbEntity entity)
        {
            string primaryKeyValue = null;
            string primaryKeyPropertyName = null;

            var columnProps = typeof(T).GetProperties().Where(
                prop => System.Attribute.IsDefined(prop, typeof(SimpleDbColumnAttribute)));

            var numberOfProperties = 0;

            foreach (var prop in columnProps)
            {
                var primaryKeyAttribute =
                    ((SimpleDbColumnAttribute[])prop.GetCustomAttributes(typeof(SimpleDbColumnAttribute), false))
                        .ToList()
                        .FirstOrDefault(x => x.IsPrimaryKey);
                if (primaryKeyAttribute != null)
                {
                    primaryKeyPropertyName = prop.Name;
                    primaryKeyValue = prop.GetValue(entity, (BindingFlags.GetProperty | BindingFlags.Instance), null,
                                                    null,
                                                    CultureInfo.InvariantCulture) as string;


                }
                numberOfProperties++;
            }

            if (numberOfProperties <= 0)
            {
                var msg =
                String.Format(
                    "The ISimpleDbEntity of type '{0}' does not contain any properties decorated with the SimpleDbColumnAttribute.",
                    (typeof(T)).ToString());
                throw new Exception(msg);
            }

            if (String.IsNullOrWhiteSpace(primaryKeyValue))
            {
                var msg =
                String.Format(
                    "The ISimpleDbEntity of type '{0}' has a property '{1}' decorated with a SimpleDbColumnAttribute of type IsPrimaryKey=true, but the value is null or blank.",
                    (typeof(T)).ToString(), primaryKeyPropertyName);
                throw new Exception(msg);
            }

            return primaryKeyValue;
        }

        public virtual string GetDomainNameOfEntity<T>()
        {
            var type = typeof (T);

            var domainAttribute =
                (SimpleDbDomainAttribute)type.GetCustomAttributes(typeof(SimpleDbDomainAttribute), false).FirstOrDefault();
            if (domainAttribute == null)
            {
                var msg =
                    String.Format(
                        "The ISimpleDbEntity of type '{0}' is not decorated with a SimpleDbDomain attribute.",
                        type.ToString());
                throw new Exception(msg);
            }
            return domainAttribute.Domain;
        }

    }
}
