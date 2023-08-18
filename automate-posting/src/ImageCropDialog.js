import React, { useState, useEffect } from "react";
import Cropper from "react-easy-crop";
import getCroppedImg from "./CropImage";
import Button from 'react-bootstrap/Button';
import cropLogo from "./Images/crop-logo.png"
import zoomLogo from './Images/zoom.png'
import facebook from './Images/facebook.png'
import instagram from './Images/instagram.png'
import twitter from './Images/twitter.avif'
import Form from 'react-bootstrap/Form';

import "./App.css"
import "./ImageGallery/ImageGallery.css"
import Modal from 'react-bootstrap/Modal';
import { Col, Row, Container } from "react-bootstrap";
import client from "./utils";

const p = {
  Facebook: facebook,
  Fnstagram: instagram,
  Twitter: twitter,
}
const aspectRatios = {
  Facebook: 1200 / 1400,
  Instagram: 1080 / 1080,
  Twitter: 1600 / 900,
}
const ImageCropDialog = ({
  selectedPlatform, open, handleClose,project,product
}) => {
 
  const aspectRatio = aspectRatios[selectedPlatform];
  const [imageLinks, setImageLinks] = useState([]);
  useEffect(() => {
    client.get(`DropBox/GetMediasInProduct/${project}/${product}`, { headers: { 'Content-Type': 'application/json', } })
      .then(response => {
        setImageLinks(response.data);
      })
      .catch(error => {
        console.error('Error fetching image links:', error);
      });
  }, []);

  const initialCropState = [];
  const initialZoomState = [];
  for (let i = 0; i < imageLinks.length; i++) {
    initialCropState.push({ x: 0, y: 0 });
    initialZoomState.push(1);
  }
  const [croppedAreaPixels, setCroppedAreaPixels] = useState([]);
  const [cropperCrop, setCropperCrop] = useState(new Array(imageLinks.length).fill({ x: 0, y: 0 }));
  const [cropperZoom, setCropperZoom] = useState(new Array(imageLinks.length).fill(1));
  for (let i = 0; i < imageLinks.length; i++) {
    cropperCrop.push({ x: 0, y: 0 });
    cropperZoom.push(1);
    croppedAreaPixels.push(null);
  }

  const VerticalLineWithShadow = () => {
    return (
      <div className="vertical-line"></div>
    );
  };


  const onCropChange = (crop, index) => {
    setCropperCrop(prevCrop => {
      var newCrop = [...prevCrop];
      newCrop[index] = { ...newCrop[index], x: crop.x, y: crop.y };

      return newCrop;
    });
  };
  const onZoomChange = (zoom, index) => {
    setCropperZoom(prevZoom => {
      var newZoom = [...prevZoom];
      newZoom[index] = zoom;
      return newZoom;
    });
  };
  const onCropComplete = (croppedArea, c, index) => {
    setCroppedAreaPixels(prevCroppedAreaPixels => {
      var newCroppedAreaPixels = [...prevCroppedAreaPixels];
      newCroppedAreaPixels[index] = c;
      return newCroppedAreaPixels;
    });
  };
  
  const onBeginProcess = async () => {
    var requestBody = {}
    imageLinks.map(async (link, index) => (
      requestBody = {
        primaryProduct: product,
        secondaryProducts: "secondaryProducts",
        imagePath: await getCroppedImg(link, croppedAreaPixels[index], selectedPlatform),
        platform: selectedPlatform
      },
      client.post("Process/begin", requestBody, {
        headers: {
          'Content-Type': 'application/json', // Use 'application/json' for JSON data
        },
      })
        .then(response => console.log("result put ", response))
        .catch(error => console.error("Error:", error)) // Use error parameter to catch errors
    ))
  }
  return (
    <Modal show={open} fullscreen={true} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>
          <Row>
            <Col xs={2}> <img style={{ width: '30px' }} src={cropLogo} /></Col>
            <Col xs={10}>  <h2> Crop Image</h2></Col>
          </Row>
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row>
          <Col xs={1}><img style={{ width: '35px' }} src={p[selectedPlatform]} /></Col> {'\u00a0'}
          <Col xs={10}>  <h4 >{selectedPlatform}</h4></Col>
        </Row><br></br>
        <Container>
          <Col xs={"auto"}></Col>
          <Col>
            <Row>
              {
                imageLinks.map((link, index) => (
                  <>
                    <Col xs="auto">
                      <Row>
                        <Cropper
                          image={link}
                          zoom={cropperZoom[index]}
                          crop={cropperCrop[index]}
                          aspect={aspectRatios[selectedPlatform]}
                          onCropChange={crop => onCropChange(crop, index)}
                          onZoomChange={zoom => onZoomChange(zoom, index)}
                          onCropComplete={(croppedArea, crop) => onCropComplete(croppedArea, crop, index)}
                          style={{
                            containerStyle: {
                              position: 'relative',
                              width: '250px',
                              height: '250px',
                            },
                            cropGuideStyle: {},
                            mediaStyle: {},
                          }
                          }
                        />  </Row><br></br>
                      <Row>
                        <input
                          type="range"
                          min={1}
                          max={3}
                          step={0.1}
                          value={cropperZoom[index]}
                          onInput={(e) => {
                            onZoomChange(e.target.value, index); // Pass index to onZoomChange
                          }}
                          className="slider"
                        ></input><br></br><br></br>
                      </Row>
                    </Col>
                    <Col xs={"auto"}></Col>
                  </>
                )
                )
              }
            </Row>
          </Col>
        </Container>
      </Modal.Body>
      <Modal.Footer >
        <Button style={{ width: '100px' }} variant="primary" onClick={() => { handleClose(); onBeginProcess() }}>
          Crop
        </Button>
        <Button style={{ width: '100px' }} variant="secondary" onClick={() => { handleClose(); }}>
          Cancel
        </Button>
      </Modal.Footer>
    </Modal>
  );



};

export default ImageCropDialog;