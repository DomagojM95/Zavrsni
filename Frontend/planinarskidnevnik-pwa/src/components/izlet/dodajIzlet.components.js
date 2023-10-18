import React, { Component } from "react";
import IzletDataService from "../../services/izleti.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";




export default class DodajIzlet extends Component {

  constructor(props) {
    super(props);
    this.dodajIzlet = this.dodajIzlet.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  async dodajIzlet(izlet) {
    const odgovor = await IzletDataService.post(izlet);
    if(odgovor.ok){
      // routing na izlete
      window.location.href='/izleti';
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

   

    this.dodajIzlet({
      naziv: podaci.get('naziv'),
      trajanje: podaci.get('trajanje'),
      datum: podaci.get('cijena'),
      planina: podaci.get('upisnina'),
     
    });
    
  }


  render() { 
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" placeholder="Naziv izleta" maxLength={255} required/>
          </Form.Group>


          <Form.Group className="mb-3" controlId="trajanje">
            <Form.Label>Trajanje</Form.Label>
            <Form.Control type="text" name="trajanje" placeholder="130" />
          </Form.Group>


         

          <Form.Group className="mb-3" controlId="datum">
            <Form.Label>Datum</Form.Label>
            <Form.Control type="text" name="datum" placeholder="50" />
          </Form.Group>

          <Form.Group className="mb-3" controlId="planina">
            <Form.Label>Planina</Form.Label>
            <Form.Control type="text" name="planina" placeholder="50" />
          </Form.Group>
          

          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/izleti`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj izlet
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

