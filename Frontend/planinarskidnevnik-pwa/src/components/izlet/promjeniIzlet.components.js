import React, { Component } from "react";
import IzletDataService from "../../services/izleti.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";



export default class PromjeniIzlet extends Component {

  constructor(props) {
    super(props);

   
    this.izlet = this.dohvatiIzlet();
    this.promjeniIzlet = this.promjeniIzlet.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    

    this.state = {
      izlet: {}
    };

  }



  async dohvatiIzlet() {
    let href = window.location.href;
    let niz = href.split('/'); 
    await IzletDataService.getBySifra(niz[niz.length-1])
      .then(response => {
        this.setState({
          izlet: response.data
        });
       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
    
   
  }

  async promjeniIzlet(izlet) {
    // ovo mora bolje
    let href = window.location.href;
    let niz = href.split('/'); 
    const odgovor = await IzletDataService.put(niz[niz.length-1],izlet);
    if(odgovor.ok){
      // routing na izlete
      window.location.href='/izleti';
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

    this.promjeniIzlet({
      naziv: podaci.get('naziv'),
      trajanje: podaci.get('trajanje'),
      datum: podaci.get('datum'),
      planina: podaci.get('planina'),
     
    });
    
  }


  render() {
    
   const { izlet} = this.state;


    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" placeholder="Naziv izleta"
            maxLength={255} defaultValue={izlet.naziv} required />
          </Form.Group>


          <Form.Group className="mb-3" controlId="datum">
            <Form.Label>Datum</Form.Label>
            <Form.Control type="text" name="datum" defaultValue={izlet.datum}  placeholder="130" />
          </Form.Group>


          <Form.Group className="mb-3" controlId="trajanje">
            <Form.Label>Trajanje</Form.Label>
            <Form.Control type="text" name="trajanje" defaultValue={izlet.trajanje}  placeholder="500" />
          </Form.Group>

          <Form.Group className="mb-3" controlId="planina">
            <Form.Label>Planina</Form.Label>
            <Form.Control type="text" name="planina" defaultValue={izlet.planina}  placeholder="50" />
          </Form.Group>

          

        
         
          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/izleti`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Promjeni izlet
            </Button>
            </Col>
          </Row>
        </Form>


      
    </Container>
    );
  }
}

