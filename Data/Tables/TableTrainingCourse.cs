
namespace Data.Model
{
    /// <summary>
    /// les formations
    /// </summary>
    public class TableTrainingCourse
    {
        /// <summary>
        /// id de la formation (incrémental)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nom de la formation
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// id de l'utilisateur propriétaire de la formation
        /// </summary>
        public int OwnerId { get; set; }


        public virtual TableUser Owner { get; set; }


    }
}
