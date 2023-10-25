import React, { Component } from "react";
import PlaninarDataService from "../../services/planinar.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";




export default class PromjeniPlaninar extends Component {

  constructor(props) {
    super(props);

    this.planinar = this.dohvatiPlaninar();
    this.promjeniPlaninar = this.promjeniPlaninar.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    
    


    this.state = {
      planinar: {}
    };
  }


  async dohvatiPlaninar() {
    // ovo mora bolje
    let href = window.location.href;
    let niz = href.split('/'); 
    await PlaninarDataService.getBySifra(niz[niz.length-1])
      .then(response => {
        this.setState({
          planinar: response.data
        });
       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }

  async promjeniPlaninar(planinar) {
    // ovo mora bolje
    let href = window.location.href;
    let niz = href.split('/'); 
    const odgovor = await PlaninarDataService.put(niz[niz.length-1],planinar);
    if(odgovor.ok){
      window.location.href='/planinar';
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

    this.promjeniPlaninar({
      ime: podaci.get('ime'),
      prezime: podaci.get('prezime'),
      oib: podaci.get('oib'),
      pldrustvo: podaci.get('pldrustvo')
    });
    
  }


  render() {
    
    const { planinar} = this.state;

    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


        <Form.Group className="mb-3" controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" placeholder="Josip" maxLength={255} defaultValue={planinar.ime} required/>
          </Form.Group>


          <Form.Group className="mb-3" controlId="prezime">
            <Form.Label>Prezime</Form.Label>
            <Form.Control type="text" name="prezime" placeholder="Horvat" defaultValue={planinar.prezime}  required />
          </Form.Group>


          <Form.Group className="mb-3" controlId="oib">
            <Form.Label>OIB</Form.Label>
            <Form.Control type="text" name="oib" placeholder="" defaultValue={planinar.oib}  />
          </Form.Group>

          <Form.Group className="mb-3" controlId="pldrustvo">
            <Form.Label>PlDrustvo</Form.Label>
            <Form.Control type="text" name="pldrustvo" placeholder="PD Đakovo" defaultValue={planinar.pldrustvo}  />
          </Form.Group>

        
         
          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/planinar`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Promjeni planinara
            </Button>
            </Col>
          </Row>
        </Form>


      
    </Container>
    );
  }
}

