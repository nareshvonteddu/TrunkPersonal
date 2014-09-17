using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FindCustomerService.AzureHelpers
{
    public class AzureMobileTable
    {
        string _tableName = string.Empty;
        private  string tableUrl {   get{   return "tables/" + _tableName;  } }

        public AzureMobileTable(string tableName)
        {
            _tableName = tableName;
        } 
        public AzureMobileQuery Where(string whereExpression)  
        { Expression convertedExpression = Expression.Constant(whereExpression);
            if (convertedExpression is MethodCallExpression)
            {
                return null;
                throw new Exception("Invalid Expression");
            } 
            return new AzureMobileQuery(tableUrl + "?$filter=" + whereExpression);  
        }
    }


}
