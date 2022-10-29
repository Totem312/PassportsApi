CREATE OR REPLACE PROCEDURE public.load_passports(IN filename character varying)
 LANGUAGE plpgsql
AS $procedure$
begin
   execute
   format('copy temppassports(series,number) from ''%s'' With CSV header', filename);
  	delete from temppassports where char_length(series)!=4 or char_length(number)!=6 ;
end $procedure$
;