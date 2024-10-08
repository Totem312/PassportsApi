
CREATE or replace procedure  tempPass()
language plpgsql
as $create$
begin 
create table temppassports (series varchar ,number varchar);
end $create$;
