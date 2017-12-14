# Elearning
step 1 - edit web.config file connection strings 

Step 2 - Run the system once and create a user 

step 3 - add roles
1. ADMIN
2. STUDENT
3. INSTRUCTOR
Note do not change spellings

step 4 - in sql server run this Queries 


create proc AddVideos
(
@name nvarchar(max),
@dis nvarchar(max),
@cID int
@path nvarchar(max)
)
as
begin
insert into [dbo].[Videos]([Name],[Discription],[Course_ID],[FilePath]) values(@name,@dis,@cID,@path)
end


create proc UpdateVideos
(
@id int,
@name nvarchar(max),
@dis nvarchar(max),
@cID int
)
as
begin
update [dbo].[Videos] set [Name]=@name,[Discription]=@dis,[Course_ID]=@cID where [ID]=@id
end


create proc UpdateVideosWithFile
(
@id int,
@name nvarchar(max),
@dis nvarchar(max),
@cID int,
@path nvarchar(max)
)
as
begin
update [dbo].[Videos] set [Name]=@name,[Discription]=@dis,[Course_ID]=@cID,[FilePath]=@path  where [ID]=@id
end


 create proc AddCourse
 (
 @name nvarchar(max),
 @discription nvarchar(max),
 @uID nvarchar(max)
 )
 as
 begin
 insert into [dbo].[Courses]([Name],[Discription],[ApplicationUser_Id]) values (@name,@discription,@uID)
 end



