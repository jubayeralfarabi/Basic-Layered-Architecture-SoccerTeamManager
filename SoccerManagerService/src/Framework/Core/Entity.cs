namespace Soccer.Platform.Infrastructure.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class Entity
    {
        [Key]
        public Guid Id { get; protected set; }

        public DateTime CreatedDate { get; protected set; }

        public DateTime LastUpdatedDate { get; protected set; }

        protected Entity()
        {
            this.Id = Guid.NewGuid();
        }

        protected Entity(Guid id)
        {
            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
            }

            this.Id = id;
        }

        public void SetId(Guid id)
        {
            this.Id = id;
        }

        public virtual void SetDefaultValue()
        {
            DateTime currentTime = DateTime.UtcNow;
            if (this.CreatedDate == null || this.CreatedDate == DateTime.MinValue)
            {
                this.CreatedDate = currentTime;
            }
            this.LastUpdatedDate = currentTime;
        }
    }
}
