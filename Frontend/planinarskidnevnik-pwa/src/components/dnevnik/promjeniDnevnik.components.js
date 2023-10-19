import React, { Component } from "react";
import DnevnikDataService from "../../services/dnevnik.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";



export default class PromjeniDnevnik extends Component {

  constructor(props) {
    super(props);

   
    this.dnevnik = this.dohvatiDnevnik();
    this.promjeniDnevnik = this.promjeniDnevnik.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    

    this.state = {
      dnevnik: {}
    };

  }



  async dohvatiDnevnik() {
    let href = window.location.href;
    let niz = href.split('/'); 
    await DnevnikDataService.getBySifra(niz[niz.length-1])
      .then(response => {
        this.setState({
          dnevnik: response.data
        });
       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
    
   
  }

  async promjeniDnevnik(dnevnik) {
    // ovo mora bolje
    let href = window.location.href;
    let niz = href.split('/'); 
    const odgovor = await DnevnikDataService.put(niz[niz.length-1],dnevnik);
    if(odgovor.ok){
      // routing na smjerovi
      window.location.href='/dnevnici';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }



  handleSubmit(e) {
    // Prevent the browser from reloading the page
    e.preventDefault();

    // Read the form data
    const podaci = new FormData(e.target);
    //Object.keys(formData).forEach(fieldName => {
    // console.log(fieldName, formData[fieldName]);
    //})
    
    //console.log(podaci.get('verificiran'));
    // You can pass formData as a service body directly:

    this.promjeniDnevnik({
      naziv: podaci.get('naziv'),
      izlet: podaci.get('izlet'),
      planinar: podaci.get('planinar')
    });
    
  }


  render() {
    
   const { dnevnik} = this.state;


    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" placeholder="Naziv smjera"
            maxLength={255} defaultValue={dnevnik.naziv} required />
          </Form.Group>


          <Form.Group className="mb-3" controlId="izlet">
            <Form.Label>Izlet</Form.Label>
            <Form.Control type="text" name="izlet" defaultValue={dnevnik.izlet}  placeholder="130" />
          </Form.Group>


          <Form.Group className="mb-3" controlId="planinar">
            <Form.Label>Planinar</Form.Label>
            <Form.Control type="text" name="planinar" defaultValue={dnevnik.planinar}  placeholder="500" />
          </Form.Group>

        

        
         
          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/dnevnici`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Promjeni dnevnik
            </Button>
            </Col>
          </Row>
        </Form>


      
    </Container>
    );
  }
}

