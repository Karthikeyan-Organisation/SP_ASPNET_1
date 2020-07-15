
/****** Object:  StoredProcedure [dbo].[Usp_UpdateLikeByBlogPostId]    Script Date: 7/15/2020 3:01:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Proc [dbo].[Usp_UpdateLikeByBlogPostId]
@BlogPostID int,
@Like int
as
begin
Update dbo.BlogPost
set BlogPost.[Like] = @Like
where BlogPostID =@BlogPostID
end
GO

