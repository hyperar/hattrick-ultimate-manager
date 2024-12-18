﻿namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Hyperar.HUM.Infrastructure.Database.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>, IEntityMapping<TEntity> where TEntity : class
    {
        private int currentColumnOrder;

        protected EntityConfigurationBase()
        {
            this.currentColumnOrder = -1;
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.MapTable(builder);
            this.MapBaseProperties(builder);
            this.MapProperties(builder);
            this.MapRelationships(builder);
        }

        public abstract void MapProperties(EntityTypeBuilder<TEntity> builder);

        public virtual void MapRelationships(EntityTypeBuilder<TEntity> builder)
        {
        }

        public abstract void MapTable(EntityTypeBuilder<TEntity> builder);

        protected int GetColumnOrder()
        {
            return this.currentColumnOrder++;
        }

        protected abstract void MapBaseProperties(EntityTypeBuilder<TEntity> builder);
    }
}