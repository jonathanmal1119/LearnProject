using Learn.Classes.Database;
using Learn.Styles.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes
{
    public class MCQManager
    {
        IDatabase database = ConnectionManager.EstablishConnection();

        string currentSet;
        int currentTotalCards;

        double numOfSubSets = 0;

        List<List<Card>> subSets = new List<List<Card>>();
        List<DataRow> cardChoicesLeft = new List<DataRow>();
        List<List<MCQ>> mcqSets = new List<List<MCQ>>();

        public MCQManager()
        {
            currentSet = "";
            currentTotalCards = 0;
            loaded();
        }

        public MCQManager(string currentSet)
        {
            this.currentSet = currentSet;
            currentTotalCards = database.GetQuery($"SELECT numOfTerms FROM StudySets WHERE grouping = \"{currentSet}\"").Rows[0].Field<int>("numOfTerms");
            loaded();
        }

        DataRowCollection QL_RowCollection;
        private void loaded()
        {
            QL_RowCollection = database.GetQuery($"SELECT term, definition, favorite FROM TermCards WHERE grouping = \"{currentSet}\"").Rows;
            foreach (DataRow item in QL_RowCollection)
            {
                cardChoicesLeft.Add(item);
            }
        }

        public void DivideSets()
        {
            Random rand = new Random();
            numOfSubSets = Math.Ceiling((double)currentTotalCards / Settings.termsPerGroupQL);
            //Console.WriteLine(numOfSubSets);

            for (int i = 0; i < numOfSubSets; i++)
            {
                List<Card> cards2 = new List<Card>();

                for (int j = 0; j < Settings.termsPerGroupQL; j++)
                {
                    int chosenIndex = rand.Next(cardChoicesLeft.Count);
                    if (cardChoicesLeft.Count > 0)
                    {
                        string term = cardChoicesLeft[chosenIndex].Field<string>("term");
                        string def = cardChoicesLeft[chosenIndex].Field<string>("definition");
                        Card tCard = new Card(term, def, currentSet);
                        cards2.Add(tCard);
                        cardChoicesLeft.Remove(cardChoicesLeft[chosenIndex]);
                    }
                    else
                    {

                    }
                }
                subSets.Add(cards2);
            }
        }

        private List<MCQ> CreateQuestion(List<Card> subset)
        {
            List<MCQ> questions = new List<MCQ>();
            for (int i = 0; i < subset.Count; i++)
            {
                MCQ mcq = new MCQ();
                mcq.generateMCQ(subset[i]);
                questions.Add(mcq);
            }
            return questions;
        }

        private void CreateMCQSets()
        {
            for (int i = 0; i < subSets.Count; i++)
            {
                mcqSets.Add(CreateQuestion(subSets[i]));
            }
        }

        public List<List<MCQ>> CreateStudySession()
        {
            DivideSets();
            CreateMCQSets();
            return mcqSets;
        }
        public void printMCQ()
        {
            MCQ mcq = new MCQ();
            //Console.WriteLine(subSets[0][0].term);
            mcq.generateMCQ(subSets[0][0]);
           
        }

    }
}
