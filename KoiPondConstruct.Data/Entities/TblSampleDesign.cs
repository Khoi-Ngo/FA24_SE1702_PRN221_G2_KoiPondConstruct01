﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KoiPondConstruct.Data.Entities;

public partial class TblSampleDesign
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public long ImgId { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime UpdatedTime { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public string ApprovedBy { get; set; }

    public DateTime ApprovedTime { get; set; }

    public string Note { get; set; }

    public string File { get; set; }

    public string ContentText { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<TblCustomerRequestDetail> TblCustomerRequestDetails { get; set; } = new List<TblCustomerRequestDetail>();

    public virtual ICollection<TblSampleDesignImgRefer> TblSampleDesignImgRefers { get; set; } = new List<TblSampleDesignImgRefer>();

    public virtual TblSuggestionDoc TblSuggestionDoc { get; set; }
}