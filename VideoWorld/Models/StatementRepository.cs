using System.Collections.Generic;

namespace VideoWorld.Models
{
    public class StatementRepository
    {
        readonly List<Statement> statements = new List<Statement>();

        public Statement FindById(int id)
        {
            return statements[id];
        }

        public int Add(Statement statement)
        {
            statements.Add(statement);
            return statements.Count - 1;
        }
    }
}