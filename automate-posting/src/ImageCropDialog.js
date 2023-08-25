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
import Modal from 'react-bootstrap/Modal';
import { Col, Row, Container, Spinner } from "react-bootstrap";
import client from "./utils";
import "./Projects/Projects"
import Projects from "./Projects/Projects";
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
  selectedPlatform, open, handleClose, project, product, onCropCompleteAndBeginProcess
}) => {

  const aspectRatio = aspectRatios[selectedPlatform];
  const [mediaRequests, setMediaRequests] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [visibleProductsReference, setVisibleProductsReference] = useState('');
  const [mediaId, setMediaId] = useState();
  const [productId, setProductId] = useState();
  const [projectId, setProjectId] = useState();

  useEffect(() => {
    setIsLoading(true);
    client.get(`DropBox/GetMediasInProduct/${project}/${product}`, {
      headers: {
        'Content-Type': 'application/json',
      },
    })
      .then(response => {
        setMediaRequests(response.data);
      })
      .catch(error => {
        console.error('Error fetching media:', error);
      })
      .finally(() => setIsLoading(false));
  }, []);
  //Get ProductId
  useEffect(() => {
    client.get(`Database/getProductIdByReference/${product}`)
      .then(response => {
        setProductId(response.data);
      })
      .catch(error => {
        console.error('Error fetching productId :', error);
      });
  }, []);
  // Get projectId
  useEffect(() => {
    client.get(`Database/getProjectIdByTitle/${project}`)
      .then(response => {
        setProjectId(response.data);
      })
      .catch(error => {
        console.error('Error fetching productId :', error);
      });
  }, []);

  const initialCropState = [];
  const initialZoomState = [];
  for (let i = 0; i < mediaRequests.length; i++) {
    initialCropState.push({ x: 0, y: 0 });
    initialZoomState.push(1);
  }
  const [croppedAreaPixels, setCroppedAreaPixels] = useState([]);
  const [cropperCrop, setCropperCrop] = useState(new Array(mediaRequests.length).fill({ x: 0, y: 0 }));
  const [cropperZoom, setCropperZoom] = useState(new Array(mediaRequests.length).fill(1));
  for (let i = 0; i < mediaRequests.length; i++) {
    cropperCrop.push({ x: 0, y: 0 });
    cropperZoom.push(1);
    croppedAreaPixels.push(null);
  }



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
  //begin the process
  const onBeginProcess = async () => {

    var requestBody = {}
    await Promise.all(
      mediaRequests.map(async (media, index) => {
        const mediaResponse = await client.get(`Database/getMediaIdByUrl/${encodeURIComponent("/shootingflow/projects/" + project + "/medias/" + product + "/" + media.name)}`);
        const mediaId = mediaResponse.data;
        console.log("mediaid", mediaId)
        console.log("productId", projectId)
        const vpresponse = await client.get(`Database/getVisibleProducts/${mediaId}/${projectId}`)
        const visibleProductsReference = vpresponse.data;
        console.log("visibleProductsReference", visibleProductsReference)
        const imagePath = await getCroppedImg(media.path, croppedAreaPixels[index], selectedPlatform);
        const requestBody = {
          primaryProduct: product,
          secondaryProducts: visibleProductsReference,
          imagePath: imagePath,
          mediaName: media.name,
          projectName: project,
          productName: product,
          platform: selectedPlatform
        };
        client.post("Process/begin", requestBody, {
          headers: {
            'Content-Type': 'application/json', // Use 'application/json' for JSON data
          },
        })
          .then(response => console.log("result put ", response))
          
          .catch(error => console.error("Error:", error)) // Use error parameter to catch errors
         
      }))
  }
  return (
    <>




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
            <Col xs="12" className="d-flex align-items-center justify-content-center text-align-center">
              <Row>
                {isLoading ? <><br></br> <Spinner style={{ width: '200px', height: '200px' }} animation="border" variant="primary" /><br></br> </>
                  : mediaRequests.map((media, index) => (
                    <>
                      <Col xs="auto">
                        <Row>
                          <Cropper
                            image={media.path}
                            zoom={cropperZoom[index]}
                            crop={cropperCrop[index]}
                            aspect={aspectRatios[selectedPlatform]}
                            onCropChange={crop => onCropChange(crop, index)}
                            onZoomChange={zoom => onZoomChange(zoom, index)}
                            onCropComplete={(croppedArea, crop) => onCropComplete(croppedArea, crop, index)}
                            style={{
                              containerStyle: {
                                position: 'relative',
                                width: '300px',
                                height: '300px',
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
          <Button style={{ width: '100px' }} variant="primary" onClick={() => { handleClose(); onBeginProcess(); onCropCompleteAndBeginProcess() }}>
            Crop
          </Button>
          <Button style={{ width: '100px' }} variant="secondary" onClick={() => { handleClose(); }}>
            Cancel
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );



};

export default ImageCropDialog;