import http from "../http-common";

class DnevnikDataService {
  getAll() {
    return http.get("/dnevnik");
  }

  async getBySifra(sifra) {
   // console.log(sifra);
    return await http.get('/dnevnik/' + sifra);
  }

  async getPlanine(sifra) {
    // console.log(sifra);
     return await http.get('/dnevnik/' + sifra + '/izlet');
   }

   async getPlaninar(sifra) {
    // console.log(sifra);
     return await http.get('/dnevnik/' + sifra + '/planinar');
   }
 


  async post(dnevnik){
    //console.log(smjer);
    const odgovor = await http.post('/dnevnik',dnevnik)
       .then(response => {
         return {ok:true, poruka: 'Unio dnevnik'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
}


  async delete(sifra){
    
    const odgovor = await http.delete('/dnevnik/' + sifra)
       .then(response => {
         return {ok:true, poruka: 'Obrisao uspješno'};
       })
       .catch(error => {
         console.log(error);
         return {ok:false, poruka: error.response.data};
       });
 
       return odgovor;
     }

     async obrisiIzlet(dnevnik, izlet){
    
      const odgovor = await http.delete('/dnevnik/obrisiizlet/' + dnevnik + '/' + izlet)
         .then(response => {
           return {ok:true, poruka: 'Obrisao uspješno'};
         })
         .catch(error => {
           console.log(error);
           return {ok:false, poruka: error.response.data};
         });
   
         return odgovor;
       }

       async obrisiPlaninar(dnevnik, planinar){
    
        const odgovor = await http.delete('/dnevnik/obrisiplaninar/' + dnevnik + '/' + planinar)
           .then(response => {
             return {ok:true, poruka: 'Obrisao uspješno'};
           })
           .catch(error => {
             console.log(error);
             return {ok:false, poruka: error.response.data};
           });
     
           return odgovor;
         }

       async dodajIzlet(dnevnik, izlet){
    
        const odgovor = await http.post('/dnevnik/dodajizlet/' + dnevnik + '/' + izlet)
           .then(response => {
             return {ok:true, poruka: 'Dodao uspješno'};
           })
           .catch(error => {
             console.log(error);
             return {ok:false, poruka: error.response.data};
           });
     
           return odgovor;
         }

         async dodajPlanianr(dnevnik, planinar){
    
          const odgovor = await http.post('/dnevnik/dodajplaninar/' + dnevnik + '/' + planinar)
             .then(response => {
               return {ok:true, poruka: 'Dodao uspješno'};
             })
             .catch(error => {
               console.log(error);
               return {ok:false, poruka: error.response.data};
             });
       
             return odgovor;
           }
  


}

export default new DnevnikDataService();