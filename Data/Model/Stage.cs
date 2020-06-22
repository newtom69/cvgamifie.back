namespace Data.Model
{
    /// <summary>
    /// les étapes des quêtes
    /// </summary>
    public class Stage
    {
        /// <summary>
        /// id de l'étape (incrémental)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// nom de l'étape
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// id de la quête de l'étape
        /// </summary>
        public int QuestId { get; set; }


        public virtual Quest Quest { get; set; }
    }
}
