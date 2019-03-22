-- get subreddits for user
select subreddit from reddit where UserID = @userID;

-- add new subreddit
insert into reddit (UserID, subreddit)
values (@userID, @subreddit);

--