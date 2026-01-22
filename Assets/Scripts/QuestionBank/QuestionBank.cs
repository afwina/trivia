using System;

namespace Game
{
    public class QuestionBank
    {
        private Question[] m_QuestionBank = new Question[]
        {
            new (){
                QuestionText = "Which of these birds are not part of the Corvidae family?", 
                Options = new []{new OptionData{OptionText = "Crows"},new OptionData{OptionText = "Magpies"},new OptionData{OptionText = "Grackles", IsCorrect = true},new OptionData{OptionText = "Blue Jays"}}
            },
            new (){
                QuestionText = "Approximately how many times a day does the human heart beat?", 
                Options = new []{new OptionData{OptionText = "100,000", IsCorrect = true},new OptionData{OptionText = "1,000,000"},new OptionData{OptionText = "25,000"},new OptionData{OptionText = "200,000"}}
            },
            new (){
                QuestionText = "Which of these storage mediums have the longest expected lifespan?", 
                Options = new []{new OptionData{OptionText = "Vinyl Record", IsCorrect = true},new OptionData{OptionText = "Floppy Disk"},new OptionData{OptionText = "HDD (Hard Disk Drive)"},new OptionData{OptionText = "Solid State Hard Drive (SSD)"}}
            },
        };
        
        private Random m_Random;
        
        public QuestionBank()
        {
            m_Random = new Random();
        }
        
        public Question GetQuestion()
        {
            int index = m_Random.Next(0, m_QuestionBank.Length);
            return m_QuestionBank[index];
        }
    }
}
