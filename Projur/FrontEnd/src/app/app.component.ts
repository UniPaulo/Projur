import { Component } from '@angular/core';
import { AppService } from './app.service';
import { FormGroup, FormControl, Validators  } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'Usuário';
  constructor(private AppService: AppService) { }
  data: any;
  UsuarioForm: FormGroup;
  submitted = false;
  EventValue: string = "Salvar";
  ngOnInit(): void {
    this.getdata();
    this.UsuarioForm = new FormGroup({
      Id: new FormControl(null),
      Nome: new FormControl("", [Validators.required]),
      Sobrenome: new FormControl("", [Validators.required]),
      Email: new FormControl("", [Validators.required]),
      DataNascimento: new FormControl("", [Validators.required]),
      Escolaridade: new FormControl("", [Validators.required]),
    })
  }

  getdata() {
    this.AppService.getData().subscribe((data: any[]) => {
      this.data = data;
    })
  }

  deleteData(id) {
    this.AppService.deleteData(id).subscribe((data: any[]) => {
      this.data = data;
      this.getdata();
    })
  }

  Save() {
    this.submitted = true;
    if (this.UsuarioForm.invalid) {
      return;
    }
    this.AppService.postData(this.UsuarioForm.value).subscribe((data: any[]) => {
      this.data = data;
      this.resetFrom();
    })
  }
  Update() {
    this.submitted = true;
    if (this.UsuarioForm.invalid) {
      return;
    }
    this.AppService.putData(this.UsuarioForm.value.PagamentoId,
      this.UsuarioForm.value).subscribe((data: any[]) => {
        this.data = data;
        this.resetFrom();
      })
  }

  EditData(Data) {
    this.UsuarioForm.controls["Id"].setValue(Data.Id);
    this.UsuarioForm.controls["Nome"].setValue(Data.Nome);
    this.UsuarioForm.controls["Sobrenome"].setValue(Data.Sobrenome);
    this.UsuarioForm.controls["Email"].setValue(Data.Email);
    this.UsuarioForm.controls["DataNascimento"].setValue(Data.DataNascimento);
    this.UsuarioForm.controls["Escolaridade"].setValue(Data.Escolaridade);
    this.EventValue = "Atualizar";
  }
  resetFrom() {
    this.getdata();
    this.UsuarioForm.reset();
    this.EventValue = "Salvar";
    this.submitted = false;
  }
}
