﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Substrate.Entities
{
    using Substrate.Nbt;

    public class EntitySquid : EntityMob
    {
        public static readonly SchemaNodeCompound SquidSchema = MobSchema.MergeInto(new SchemaNodeCompound("")
        {
            new SchemaNodeString("id", "Squid"),
        });

        public EntitySquid ()
            : base("Squid")
        {
        }

        public EntitySquid (TypedEntity e)
            : base(e)
        {
        }


        #region INBTObject<Entity> Members

        public override bool ValidateTree (TagNode tree)
        {
            return new NbtVerifier(tree, SquidSchema).Verify();
        }

        #endregion


        #region ICopyable<Entity> Members

        public override TypedEntity Copy ()
        {
            return new EntitySquid(this);
        }

        #endregion
    }
}
