import http from '../http-common';

class PlaninaDataService {
  async getAll() {
    return await http.get('/Planina');
  }



  async getBySifra(sifra) {
    return await http.get('/Planina/' + sifra);
  }

  async post(planina){
    //console.log(smjer);
    const odgovor = await http.post('/planina',planina)
       .then(response => {
         return {ok:true, poruka: 'Unio planinu'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
  }

  async put(sifra,planina){
    const odgovor = await http.put('/planina/' + sifra,planina)
       .then(response => {
         return {ok:true, poruka: 'Promjenio planinu'}; // return u odgovor
       })
       .catch(error => {
        console.log(error.response);
         return {ok:false, poruka: error.response.data}; // return u odgovor
       });
 
       return odgovor;
     }


  async delete(sifra){
    
    const odgovor = await http.delete('/planina/' + sifra)
       .then(response => {
         return {ok:true, poruka: 'Obrisao uspješno'};
       })
       .catch(error => {
         console.log(error);
         return {ok:false, poruka: error.response.data};
       });
 
       return odgovor;
     }


     async traziPlanina(uvjet) {
      console.log('Tražim s: ' + uvjet);
      return await http.get('/planina/trazi/'+uvjet);
    }
     
 
}

export default new PlaninaDataService();