import http from '../http-common';

class PlaninarDataService {
  async getAll() {
    return await http.get('/planinar');
  }



  async getBySifra(sifra) {
    return await http.get('/planinar/' + sifra);
  }

  async post(planinar){
    //console.log(smjer);
    const odgovor = await http.post('/planinar',planinar)
       .then(response => {
         return {ok:true, poruka: 'Unio planinara'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
  }

  async put(sifra,planinar){
    const odgovor = await http.put('/planinar/' + sifra,planinar)
       .then(response => {
         return {ok:true, poruka: 'Promjenio planinara'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
     }


  async delete(sifra){
    
    const odgovor = await http.delete('/planinar/' + sifra)
       .then(response => {
         return {ok:true, poruka: 'Obrisao uspješno'};
       })
       .catch(error => {
         console.log(error);
         return {ok:false, poruka: error.response.data};
       });
 
       return odgovor;
     }


     async traziPlaninar(uvjet) {
      console.log('Tražim s: ' + uvjet);
      return await http.get('/planinar/trazi/'+uvjet);
    }
     
 
}

export default new PlaninarDataService();