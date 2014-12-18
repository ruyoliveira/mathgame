<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Welcome extends CI_Controller {

	/**
	 * Index Page for this controller.
	 *
	 * Maps to the following URL
	 * 		http://example.com/index.php/welcome
	 *	- or -  
	 * 		http://example.com/index.php/welcome/index
	 *	- or -
	 * Since this controller is set as the default controller in 
	 * config/routes.php, it's displayed at http://example.com/
	 *
	 * So any other public methods not prefixed with an underscore will
	 * map to /index.php/welcome/<method_name>
	 * @see http://codeigniter.com/user_guide/general/urls.html
	 */
	public function index()
	{
		$this->load->view('header.php');
		$this->load->view('home.php');
	}
	public function desafios($idDesafio = "")
	{
		if($idDesafio != "")
		{
			$data = array('idDesafio' => $idDesafio);
			$this->load->view('header.php');
			$this->load->view('estatisticadesafios.php', $data);
		}
		else {
			$this->load->view('header.php');
			$this->load->view('desafios.php');
		}
	}
	public function alunos()
	{
		$this->load->view('header.php');
		$this->load->view('alunos.php');
	}
	public function login()
	{
		$this->load->view('login.php');
	}
	public function gerador()
	{
		$this->load->view('temp_gerador.php');
	} 
}

/* End of file welcome.php */
/* Location: ./application/controllers/welcome.php */