﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiPondConstruct.Data.Entities;

public partial class TblImgRefer
{
    public long Id { get; set; }

    public string ImgUrl { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime DeletedTime { get; set; }

    public virtual ICollection<TblSampleDesignImgRefer> TblSampleDesignImgRefers { get; set; } = new List<TblSampleDesignImgRefer>();
}