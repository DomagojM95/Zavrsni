import React, { Component } from "react";
import DnevnikDataService from "../../services/dnevnik.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";




export default class DodajDnevnik extends Component {

  constructor(props) {
    super(props);
    this.dodajDnevnik = this.dodajDnevnik.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  async dodajDnevnik(dnevnik) {
    const odgovor = await DnevnikDataService.post(dnevnik);
    if(odgovor.ok){
      // routing na dnevnici
      window.location.href='/dnevnici';
    }else{
      // pokaži grešku
     // console.log(odgovor.poruka.errors);
      let poruke = '';
      for (const key in odgovor.poruka.errors) {
        if (odgovor.poruka.errors.hasOwnProperty(key)) {
          poruke += `${odgovor.poruka.errors[key]}` + '\n';
         // console.log(`${key}: ${odgovor.poruka.errors[key]}`);
        }
      }

      alert(poruke);
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

    this.dodajDnevnik({
      naziv: podaci.get('naziv'),
      izlet: podaci.get('izlet'),
      planinar: podaci.get('planinar')
    });
    
  }


  render() { 
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" placeholder="Naziv dnevnika" maxLength={255} required/>
          </Form.Group>


          <Form.Group className="mb-3" controlId="izlet">
            <Form.Label>Izlet</Form.Label>
            <Form.Control type="text" name="izlet" placeholder="130" />
          </Form.Group>


          
          <Form.Group className="mb-3" controlId="planinar">
            <Form.Label>Planinar</Form.Label>
            <Form.Control type="text" name="planinar" placeholder="50" />
          </Form.Group>


          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/dnevnici`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj dnevnik
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

