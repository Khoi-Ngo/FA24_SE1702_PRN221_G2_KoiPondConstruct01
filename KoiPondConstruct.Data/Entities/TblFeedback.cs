﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiPondConstruct.Data.Entities;

public partial class TblFeedback
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string AttachedFile { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime UpdatedTime { get; set; }

    public bool IsSolved { get; set; }

    public bool IsDeleted { get; set; }

    public long UserId { get; set; }

    public long SuggestionDocId { get; set; }

    public virtual TblSuggestionDoc SuggestionDoc { get; set; }

    public virtual TblUser User { get; set; }
}