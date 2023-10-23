import http from "../http-common";

class IzletDataService {
  getAll() {
    return http.get("/izlet");
  }

  async getBySifra(sifra) {
   // console.log(sifra);
    return await http.get('/izlet/' + sifra);
  }

  async getPlanine(sifra) {
    // console.log(sifra);
     return await http.get('/izlet/' + sifra + '/planina');
   }
 


  async post(izlet){
    //console.log(smjer);
    const odgovor = await http.post('/izlet',izlet)
       .then(response => {
         return {ok:true, poruka: 'Unio izlet'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
}


  async delete(sifra){
    
    const odgovor = await http.delete('/izlet/' + sifra)
       .then(response => {
         return {ok:true, poruka: 'Obrisao uspješno'};
       })
       .catch(error => {
         console.log(error);
         return {ok:false, poruka: error.response.data};
       });
 
       return odgovor;
     }

     async obrisiPlaninu(izlet, planina){
    
      const odgovor = await http.delete('/izlet/obrisiplaninu/' + izlet + '/' + planina)
         .then(response => {
           return {ok:true, poruka: 'Obrisao uspješno'};
         })
         .catch(error => {
           console.log(error);
           return {ok:false, poruka: error.response.data};
         });
   
         return odgovor;
       }

       async dodajPlaninu(izlet, planina){
    
        const odgovor = await http.post('/izlet/dodajplaninu/' + izlet + '/' + planina)
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

export default new IzletDataService();