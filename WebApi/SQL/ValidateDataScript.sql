create or replace procedure validdata ()
language plpgsql
as $createtable$
begin 
insert into passports (series,"number","Id",status)
SELECT 
nullif(regexp_replace(series, '[^\.\d]','','g'), '')::int AS series, 
nullif(regexp_replace(number, '[^\.\d]','','g'), '')::int AS number,
concat(series,number),
false
FROM temppassports t 
where not exists(Select concat(t.series,t.number)
 from passports as p where concat(t.series,t.number)=p."Id");
end $createtable$;