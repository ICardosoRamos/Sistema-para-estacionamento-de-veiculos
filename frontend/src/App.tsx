import React from "react";
import "./App.css";
import logoEstacionamento from "./assets/logo_estacionamento.svg";
import {
  Box,
  Button,
  Tabs,
  Tab,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  DialogTitle,
  DialogContent,
  Dialog,
  DialogActions,
  TextField,
} from "@mui/material";

import axios from "axios";

interface SimpleDialogProps {
  open: boolean;
  onClose: () => void;
  setEstacionamentos: React.Dispatch<
    React.SetStateAction<TEstacionamento[] | null>
  >;
}

function MarcarEntradaDialog(props: SimpleDialogProps) {
  const { onClose, open } = props;
  const [placa, setPlaca] = React.useState("");
  const [dataEntrada, setDataEntrada] = React.useState("");

  const handleClose = () => {
    onClose();
  };

  const handleSubmit = () => {
    axios
      .post("https://localhost:7220/api/Estacionamento", {
        placa: placa,
        horarioChegada: dataEntrada,
      })
      .then((response) => {
        if (response.status === 201) {
          axios
            .get("https://localhost:7220/api/Estacionamento")
            .then((response) => {
              props.setEstacionamentos(
                (response.data as TEstacionamento[])
                  .map((estacionamento, index) => {
                    if (!estacionamento.horarioSaida) {
                      const horarioChegada =
                        estacionamento.horarioChegada.substring(0, 10);

                      const partsHorarioChegada = horarioChegada.split("-");

                      const formattedDateHorarioChegada = `${partsHorarioChegada[2]}/${partsHorarioChegada[1]}/${partsHorarioChegada[0]}`;

                      return {
                        ...estacionamento,
                        item: index + 1,
                        horarioChegada: formattedDateHorarioChegada,
                        precoHora: new Intl.NumberFormat("pt-BR", {
                          style: "currency",
                          currency: "BRL",
                        }).format(Number(estacionamento.precoHora)),
                      };
                    } else {
                      const horarioChegada =
                        estacionamento.horarioChegada.substring(0, 10);
                      const horarioSaida =
                        estacionamento.horarioSaida.substring(0, 10);
                      const duracao = formatDuration(estacionamento.duracao);
                      const tempoCobrado = convertToHours(
                        String(estacionamento.tempoCobrado)
                      );

                      const partsHorarioChegada = horarioChegada.split("-");
                      const partsHorarioSaida = horarioSaida.split("-");

                      const formattedDateHorarioChegada = `${partsHorarioChegada[2]}/${partsHorarioChegada[1]}/${partsHorarioChegada[0]}`;
                      const formattedDateHorarioSaida = `${partsHorarioSaida[2]}/${partsHorarioSaida[1]}/${partsHorarioSaida[0]}`;

                      return {
                        ...estacionamento,
                        item: index + 1,
                        horarioChegada: formattedDateHorarioChegada,
                        horarioSaida: formattedDateHorarioSaida,
                        duracao: duracao,
                        tempoCobrado: tempoCobrado,
                        precoHora: new Intl.NumberFormat("pt-BR", {
                          style: "currency",
                          currency: "BRL",
                        }).format(Number(estacionamento.precoHora)),
                        valorAPagar: new Intl.NumberFormat("pt-BR", {
                          style: "currency",
                          currency: "BRL",
                        }).format(Number(estacionamento.valorAPagar)),
                      };
                    }
                  })
                  .sort((a, b) => {
                    if (a.horarioSaida === null && b.horarioSaida !== null) {
                      return -1;
                    } else if (
                      a.horarioSaida !== null &&
                      b.horarioSaida === null
                    ) {
                      return 1;
                    } else {
                      return 0;
                    }
                  })
              );
            })
            .catch((error) => {
              console.log(error);
            });
        }
      })
      .catch((error) => [console.log(error)]);

    setPlaca("");
    setDataEntrada("");
    handleClose();
  };

  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>Marcar entrada de veículos</DialogTitle>
      <DialogContent
        style={{
          width: "350px",
          height: "200px",
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          boxSizing: "border-box",
          flexDirection: "column",
          gap: 10,
        }}
      >
        <Box display={"flex"} width={"100%"}>
          <TextField
            fullWidth
            label={"Placa do Veículo"}
            variant="standard"
            value={placa}
            onChange={(e) => setPlaca(e.target.value)}
          />
        </Box>
        <Box width={"100%"}>
          <TextField
            fullWidth
            label={"Horário Chegada"}
            type="date"
            variant="standard"
            value={dataEntrada}
            onChange={(e) => {
              setDataEntrada(e.target.value);
            }}
          />
          <Button onClick={() => setDataEntrada("")}>
            Limpar Horário Chegada
          </Button>
        </Box>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={() => handleSubmit()}
          variant="contained"
          style={{ backgroundColor: "#001d9e" }}
        >
          Confirmar
        </Button>
        <Button onClick={handleClose}>Cancelar</Button>
      </DialogActions>
    </Dialog>
  );
}

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function CustomTabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && <Box sx={{ p: 3 }}>{children}</Box>}
    </div>
  );
}

function a11yProps(index: number) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

type TEstacionamento = {
  id: number;
  item: number;
  placa: string;
  horarioChegada: string;
  horarioSaida: string;
  duracao: string;
  tempoCobrado: number;
  precoHora: string;
  valorAPagar: string;
};

function App() {
  const [tab, setTab] = React.useState(0);
  const [currentTime, setCurrentTime] = React.useState(new Date());
  const [estacionamentos, setEstacionamentos] = React.useState<
    TEstacionamento[] | null
  >(null);
  const [open, setOpen] = React.useState(false);

  const handleClickOpenMarcarEntrada = () => {
    setOpen(true);
  };

  const handleCloseDialog = () => {
    setOpen(false);
  };

  const handleChangeTab = (newValue: number) => {
    setTab(newValue);
  };

  React.useEffect(() => {
    const intervalId = setInterval(() => {
      setCurrentTime(new Date());
    }, 1000);

    return () => clearInterval(intervalId);
  }, []);

  const formatDate = (date: Date) => {
    return new Intl.DateTimeFormat("pt-BR", {
      weekday: "long",
      day: "2-digit",
      month: "long",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
      second: "2-digit",
    }).format(date);
  };

  return (
    <div
      style={{
        width: "95vw",
        height: "85vh",
        backgroundColor: "#85c0ff",
        borderRadius: 12,
      }}
    >
      <Box display={"flex"} flexDirection={"row"} height={"16vh"}>
        <Box flex={2.5} flexDirection={"row"} display={"flex"}>
          <img src={logoEstacionamento} alt="React logo" />
          <Box
            display={"flex"}
            flexDirection={"column"}
            alignItems={"flex-end"}
          >
            <h3>Estacionamento</h3>
            <h1>
              <i>Carro Bem Guardado</i>
            </h1>
          </Box>
        </Box>
        <Box flex={1}>
          <h3>{formatDate(currentTime)}</h3>
          <Box display={"flex"} gap={1}>
            <Button
              variant="contained"
              style={{ backgroundColor: "#001d9e" }}
              onClick={handleClickOpenMarcarEntrada}
            >
              Marcar Entrada
            </Button>
            <MarcarEntradaDialog
              setEstacionamentos={setEstacionamentos}
              open={open}
              onClose={handleCloseDialog}
            />
          </Box>
        </Box>
      </Box>

      <Box>
        <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
          <Tabs
            value={tab}
            onChange={(_, newValue) => handleChangeTab(newValue)}
            aria-label="basic tabs example"
          >
            <Tab label="Registar Estacionamentos" {...a11yProps(0)} />
            <Tab label="Preços" {...a11yProps(1)} />
          </Tabs>
        </Box>
        <CustomTabPanel value={tab} index={0}>
          <Estacionamentos
            estacionamentos={estacionamentos as TEstacionamento[]}
            setEstacionamentos={setEstacionamentos}
          />
        </CustomTabPanel>
        <CustomTabPanel value={tab} index={1}>
          <Precos />
        </CustomTabPanel>
      </Box>
    </div>
  );
}

export default App;

type TPreco = {
  item: number;
  inicioVigencia: string;
  fimVigencia: string;
  valorHora: string;
};

function Precos() {
  const [precos, setPrecos] = React.useState<TPreco[] | null>(null);

  React.useEffect(() => {
    axios
      .get("https://localhost:7220/api/Preco")
      .then((response) => {
        setPrecos(
          (response.data as TPreco[]).map((preco, index) => {
            const inicioVigencia = preco.inicioVigencia.substring(0, 10);
            const fimVigencia = preco.fimVigencia.substring(0, 10);

            const partsInicioVigencia = inicioVigencia.split("-");
            const partsFimVigencia = fimVigencia.split("-");

            const formattedDateInicioVigencia = `${partsInicioVigencia[2]}/${partsInicioVigencia[1]}/${partsInicioVigencia[0]}`;
            const formattedDateFimVigencia = `${partsFimVigencia[2]}/${partsFimVigencia[1]}/${partsFimVigencia[0]}`;

            return {
              ...preco,
              item: index + 1,
              inicioVigencia: formattedDateInicioVigencia,
              fimVigencia: formattedDateFimVigencia,
              valorHora: new Intl.NumberFormat("pt-BR", {
                style: "currency",
                currency: "BRL",
              }).format(Number(preco.valorHora)),
            };
          })
        );
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  return (
    <Box>
      <TableContainer
        component={Paper}
        style={{ height: "55vh", overflow: "auto" }}
      >
        <Table sx={{ minWidth: 650 }} aria-label="simple table" stickyHeader>
          <TableHead>
            <TableRow>
              <TableCell>Item</TableCell>
              <TableCell align="right">Inicio Vigência</TableCell>
              <TableCell align="right">Fim Vigência</TableCell>
              <TableCell align="right">Valor por Hora</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {precos?.map((preco, index) => (
              <TableRow
                key={`${preco.item}-${index}`}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {preco.item}
                </TableCell>
                <TableCell align="right">{preco.inicioVigencia}</TableCell>
                <TableCell align="right">{preco.fimVigencia}</TableCell>
                <TableCell align="right">{preco.valorHora}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
}

function formatDuration(duration: string) {
  const [daysPart, timePart] = duration.split(".");

  const days = parseInt(daysPart, 10);

  const [hours, minutes, seconds] = timePart.split(":");

  return `${days}d ${parseInt(hours, 10)}hrs ${parseInt(minutes, 10)}m`;
}

function convertToHours(duration: string) {
  const [daysPart, timePart] = duration.split(".");

  const days = parseInt(daysPart, 10);
  const hoursFromDays = days * 24;

  const [hours, minutes, seconds] = timePart.split(":");

  const hoursInt = parseInt(hours, 10);

  const totalHours = hoursFromDays + hoursInt;

  return totalHours;
}

function Estacionamentos({
  estacionamentos,
  setEstacionamentos,
}: {
  estacionamentos: TEstacionamento[];
  setEstacionamentos: React.Dispatch<
    React.SetStateAction<TEstacionamento[] | null>
  >;
}) {
  const handleGetEstacionamentos = () => {
    axios
      .get("https://localhost:7220/api/Estacionamento")
      .then((response) => {
        setEstacionamentos(
          (response.data as TEstacionamento[])
            .map((estacionamento, index) => {
              if (!estacionamento.horarioSaida) {
                const horarioChegada = estacionamento.horarioChegada.substring(
                  0,
                  10
                );

                const partsHorarioChegada = horarioChegada.split("-");

                const formattedDateHorarioChegada = `${partsHorarioChegada[2]}/${partsHorarioChegada[1]}/${partsHorarioChegada[0]}`;

                return {
                  ...estacionamento,
                  item: index + 1,
                  horarioChegada: formattedDateHorarioChegada,
                  precoHora: new Intl.NumberFormat("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  }).format(Number(estacionamento.precoHora)),
                };
              } else {
                const horarioChegada = estacionamento.horarioChegada.substring(
                  0,
                  10
                );
                const horarioSaida = estacionamento.horarioSaida.substring(
                  0,
                  10
                );
                const duracao = formatDuration(estacionamento.duracao);
                const tempoCobrado = convertToHours(
                  String(estacionamento.tempoCobrado)
                );

                const partsHorarioChegada = horarioChegada.split("-");
                const partsHorarioSaida = horarioSaida.split("-");

                const formattedDateHorarioChegada = `${partsHorarioChegada[2]}/${partsHorarioChegada[1]}/${partsHorarioChegada[0]}`;
                const formattedDateHorarioSaida = `${partsHorarioSaida[2]}/${partsHorarioSaida[1]}/${partsHorarioSaida[0]}`;

                return {
                  ...estacionamento,
                  item: index + 1,
                  horarioChegada: formattedDateHorarioChegada,
                  horarioSaida: formattedDateHorarioSaida,
                  duracao: duracao,
                  tempoCobrado: tempoCobrado,
                  precoHora: new Intl.NumberFormat("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  }).format(Number(estacionamento.precoHora)),
                  valorAPagar: new Intl.NumberFormat("pt-BR", {
                    style: "currency",
                    currency: "BRL",
                  }).format(Number(estacionamento.valorAPagar)),
                };
              }
            })
            .sort((a, b) => {
              if (a.horarioSaida === null && b.horarioSaida !== null) {
                return -1;
              } else if (a.horarioSaida !== null && b.horarioSaida === null) {
                return 1;
              } else {
                return 0;
              }
            })
        );
      })
      .catch((error) => {
        console.log(error);
      });
  };

  React.useEffect(() => {
    handleGetEstacionamentos();
  }, []);

  return (
    <Box>
      <TableContainer
        component={Paper}
        style={{ height: "55vh", overflow: "auto" }}
      >
        <Table sx={{ minWidth: 650 }} aria-label="simple table" stickyHeader>
          <TableHead>
            <TableRow>
              <TableCell>Item</TableCell>
              <TableCell align="right">Placa</TableCell>
              <TableCell align="right">Horario de Chegada</TableCell>
              <TableCell align="right">Situação</TableCell>
              <TableCell align="right">Horario de Saída</TableCell>
              <TableCell align="right">Duração</TableCell>
              <TableCell align="right">Tempo Cobrado (Horas)</TableCell>
              <TableCell align="right">Preço da Hora</TableCell>
              <TableCell align="right">Valor à Pagar</TableCell>
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {estacionamentos?.map((estacionamento, index) => (
              <TableRow
                key={`${estacionamento.item}-${index}`}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {estacionamento.item}
                </TableCell>
                <TableCell align="right">{estacionamento.placa}</TableCell>
                <TableCell align="right">
                  {estacionamento.horarioChegada}
                </TableCell>
                <TableCell align="right">
                  {estacionamento.horarioSaida ? "Já saiu" : "Não saiu ainda"}
                </TableCell>
                <TableCell align="right">
                  {estacionamento.horarioSaida}
                </TableCell>
                <TableCell align="right">{estacionamento.duracao}</TableCell>
                <TableCell align="right">
                  {estacionamento.tempoCobrado}
                </TableCell>
                <TableCell align="right">{estacionamento.precoHora}</TableCell>
                <TableCell align="right">
                  {estacionamento.valorAPagar}
                </TableCell>
                <TableCell align="right">
                  {estacionamento.horarioSaida === null ? (
                    <Button
                      size="small"
                      onClick={() => {
                        axios
                          .put(
                            `https://localhost:7220/api/Estacionamento/${estacionamento.id}`
                          )
                          .then((response) => {
                            return handleGetEstacionamentos();
                          })
                          .catch((error) => {
                            console.log(error);
                          });
                      }}
                      variant="contained"
                      style={{ backgroundColor: "#dc0000" }}
                    >
                      Marcar Saída
                    </Button>
                  ) : (
                    <></>
                  )}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
}
