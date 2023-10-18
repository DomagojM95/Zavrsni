import http from "../http-common";


class IzletDataService{

    async get(){
        return await http.get('/Izlet');
    }

    async getBySifra(sifra) {
        return await http.get('/Izlet/' + sifra);
      }

    async delete(sifra){
        const odgovor = await http.delete('/Izlet/' + sifra)
        .then(response => {
            return {ok: true, poruka: 'Obrisao uspješno'};
        })
        .catch(e=>{
            return {ok: false, poruka: e.response.data};
        });

        return odgovor;
    }


    async post(izlet){
        //console.log(smjer);
        const odgovor = await http.post('/izlet',izlet)
           .then(response => {
             return {ok:true, poruka: 'Unio izlet'}; // return u odgovor
           })
           .catch(error => {
            //console.log(error.response);
             return {ok:false, poruka: error.response.data}; // return u odgovor
           });
     
           return odgovor;
    }

    async put(sifra,izlet){
        //console.log(smjer);
        const odgovor = await http.put('/izlet/' + sifra,izlet)
           .then(response => {
             return {ok:true, poruka: 'Promjenio izlet'}; // return u odgovor
           })
           .catch(error => {
            //console.log(error.response);
             return {ok:false, poruka: error.response.data}; // return u odgovor
           });
     
           return odgovor;
         }

}

export default new IzletDataService();