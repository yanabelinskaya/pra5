//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace prakt5
{
    using System;
    using System.Collections.Generic;
    
    public partial class Composition
    {
        public int Composition_id { get; set; }
        public string Composition1 { get; set; }
        public Nullable<int> product_id { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
