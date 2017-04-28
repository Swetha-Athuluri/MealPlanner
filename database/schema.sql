-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

--BEGIN;

-- CREATE statements go here

--COMMIT;
--insert into meal_recipe values(1,7,4,'Side dish');

--create table meal(
--meal_id INT IDENTITY (1,1) Primary key,
--meal_name varchar(100)
--);
--create table meal_recipe(
--meal_recipe_id INT IDENTITY (1,1) ,
--meal_name varchar(100),
--meal_id int,
--recipe_id int,
--user_id int,
--constraint pk_meal_recipe primary key (meal_recipe_id),
--constraint fk_mr_meal foreign key (meal_id) references meal(meal_id),
--constraint fk_mr_recipe foreign key (recipe_id) references recipe(recipe_id),
--constraint fk_mr_user foreign key (user_id) references users(user_id)
--);
--alter table meal_recipe drop column meal_name ;
--alter table meal_recipe add meal_type varchar(100);

--Insert into meal (meal_name) values('meal_test');
--Insert into meal (meal_name) values('meal_test1');
--Insert into meal (meal_name) values('meal_test2');
--Insert into meal (meal_name) values('meal_test3');
--Insert into meal_recipe values(1,1,4,'Main dish');
--Insert into meal_recipe values(1,2,4,'Side dish');
--Insert into meal_recipe values(1,6,4,'Desert');
--Insert into meal_recipe values(1,1,4,'Main dish');
--Insert into meal_recipe values(1,2,4,'Side dish');
--Insert into meal_recipe values(1,6,4,'Desert');


--select meal.meal_name,recipe.recipe_name,meal_recipe.meal_type from meal_recipe 
--inner join meal on meal.meal_id=meal_recipe.meal_id
--inner join recipe on recipe.recipe_id=meal_recipe.recipe_id
--inner join users on users.user_id=meal_recipe.user_id
--and users.user_id=4;


--create table recipe(
--recipe_id INT IDENTITY (1,1) Primary Key,
--recipe_name varchar(255) ,
--recipe_type varchar(255),
--image_name varchar(255),
--recipe_description varchar(255),
--cook_time int,
--user_id int
--);
--alter table recipe add constraint fk_recipe_user foreign key (user_id) references users (user_id);

--alter table recipe add user_id int;
--alter table recipe add constraint fk_recipe_user foreign key (user_id) references users (user_id);

--CREATE TABLE users(
--user_id INT IDENTITY (1,1) Primary Key,
--userName VARCHAR (256),
--email VARCHAR(255) NOT NULL,
--password varchar(32) NOT NULL
--);

--INSERT INTO users (userName, email, password) VALUES ('TestABC123', 'test@test.com', 'ABC123');
--select * from users

--create table recipe_ingredient(
--recipe_id int,
--ingredient_id int,
--quantity int not null,

--constraint pk_recipe_ingredient primary key (recipe_id, ingredient_id)
--);

--alter table recipe_ingredient add measurement varchar(255) not null;
--alter table recipe_ingredient add constraint fk_rid_ritable foreign key (recipe_id) references recipe(recipe_id);
--alter table recipe_ingredient add constraint fk_ingid_ritable foreign key (ingredient_id) references ingredient(ingredient_id);

--create table preparation_steps(
--step_id INT IDENTITY (1,1) ,
--recipe_id int ,
--steps varchar(255) not null,
--constraint pk_preparation_steps primary key (step_id, recipe_id),
--constraint fk_preparation_steps foreign key (recipe_id) references recipe(recipe_id)
--);

--select * from users
--set IDENTITY_INSERT OFF

--alter table recipe_ingredient drop UQ__recipe_i__9D97EE51CD734DC0



-- These are the tables for mealplanner Database



CREATE TABLE users
(
user_id INT IDENTITY (1,1) Primary Key,
userName VARCHAR (256),
email VARCHAR(255) NOT NULL,
password varchar(32) NOT NULL,
salt VARCHAR(64)
);


CREATE TABLE recipe
(
recipe_id INT IDENTITY(1,1),
recipe_name VARCHAR(255) not null,
recipe_type VARCHAR(255) not null,
image_name varchar(255) NULL,
recipe_description VARCHAR(1000) not null,
cook_time int,
user_id int,

Constraint pk_recipe_id primary key(recipe_id),
constraint fk_user_recipe foreign key(user_id) references users(user_id)
);

CREATE TABLE recipe_ingredient
(
recipe_id int,
ingredient_name Varchar(500),
quantity varchar(100),
measurement varchar(100),

constraint pk_rid_iname primary key(recipe_id,ingredient_name)

);

create table preparation_steps
(
step_id INT IDENTITY (1,1),
recipe_id int,
steps varchar(1000) not null,

constraint pk_preparation_steps primary key (step_id, recipe_id),
constraint fk_preparation_steps foreign key (recipe_id)references recipe(recipe_id)
);


create table meal
(
meal_id INT IDENTITY (1,1),
meal_name varchar(255)

constraint pk_mealid primary key(meal_id)
);

create table meal_recipe
(
meal_recipe_id int IDENTITY(1,1),
meal_id int,
recipe_id int,
user_id int,
meal_type varchar(255),

constraint pk_meal_recipe_id primary key(meal_recipe_id),
constraint fk_mr_meal foreign key (meal_id) references meal(meal_id),
constraint fk_mr_recipe foreign key (recipe_id) references recipe(recipe_id),
constraint fk_mr_user foreign key (user_id) references users(user_id)

);

--create table meal_planner
--(
-- meal_date date,
-- meal_id  int,
-- user_id int,
-- meal_planner_name varchar(500),


-- constraint pk_meal_planner primary key(meal_date,meal_id,user_id,meal_planner_name),
-- constraint fk_meal_id foreign key(meal_id) references meal(meal_id),
-- constraint fk_user_id foreign key(user_id) references users(user_id),
-- constraint chk_meal_date check(meal_date >= getdate())

-- );

create table meal_planner
(
  mealplanner_id int IDENTITY(1,1),
  mealplanner_name varchar(1000),
  user_id int,

  constraint pk_meal_planner primary key(mealplanner_id),
  constraint fk_meal_planner foreign key(user_id) references users(user_id)
  );

  create table meal_meal_planner
  (
    mealplanner_id int,
	meal_id int,
	meal_date date,
	meal_time_of_day varchar(50),

	constraint pk_meal_meal_planner primary key(mealplanner_id,meal_id,meal_date,meal_time_of_day),
	constraint fk_meal_meal_planner_meal foreign key(meal_id) references meal(meal_id)
	);