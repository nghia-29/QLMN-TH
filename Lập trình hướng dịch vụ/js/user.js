var app = angular.module('AppUser', []);

app.controller('UserController', function($scope, $http) {

    // =============================================================
    // 1. CẤU HÌNH API
    // =============================================================
    // Lưu ý: Đổi cổng 6002 thành cổng thực tế Backend đang chạy trên máy bạn
    var host = "http://localhost:6003"; 
    
    // Giả lập mã học sinh sau khi đăng nhập (Thực tế sẽ lấy từ localStorage.getItem('user'))
    var currentMaHS = "HS01"; 

    $scope.currentTab = 'hs'; // Tab mặc định là Hồ sơ
    
    // Khởi tạo biến chứa dữ liệu
    $scope.hocSinhInfo = {};
    $scope.dsDiem = [];
    $scope.dsSucKhoe = [];
    $scope.dsThongBao = [];

    // =============================================================
    // 2. HÀM CHUYỂN TAB VÀ GỌI API (LAZY LOAD)
    // =============================================================
    $scope.changeTab = function(tabName) {
        $scope.currentTab = tabName;
        
        // Chỉ gọi API khi biến dữ liệu đang rỗng để tối ưu tốc độ
        if(tabName == 'diem' && $scope.dsDiem.length == 0) {
            $scope.loadBangDiem();
        }
        if(tabName == 'suckhoe' && $scope.dsSucKhoe.length == 0) {
            $scope.loadSucKhoe();
        }
        if(tabName == 'thongbao' && $scope.dsThongBao.length == 0) {
            $scope.loadThongBao();
        }
    };

    // =============================================================
    // 3. GỌI API THÔNG TIN HỌC SINH
    // =============================================================
    $scope.loadThongTin = function() {
        // Gọi API HocSinhController (Giả định bạn đã có API này)
        $http.get(host + "/api/HocSinh/get-by-id?id=" + currentMaHS).then(function(res) {
            $scope.hocSinhInfo = res.data;
        }, function(err) {
            console.error("Lỗi tải thông tin HS:", err);
        });
    };

    // =============================================================
    // 4. GỌI API BẢNG ĐIỂM (BangDiemController)
    // =============================================================
    $scope.loadBangDiem = function() {
        // API C#: [HttpGet("get-by-hs")] public IActionResult GetByHocSinh(string maHS)
        var url = host + "/api/BangDiem/get-by-hs?maHS=" + currentMaHS;

        $http.get(url).then(function(res) {
            $scope.dsDiem = res.data;
            // Dữ liệu trả về gồm: MaBangDiem, MaMonHoc, NamHoc, HocKy, DiemSo, NhanXet...
        }, function(err) {
            console.error("Lỗi tải bảng điểm:", err);
        });
    };

    // =============================================================
    // 5. GỌI API SỨC KHỎE (HosoSucKhoeController)
    // =============================================================
    $scope.loadSucKhoe = function() {
        // API C#: [HttpGet("get-by-hs")] public IActionResult GetByHocSinh(string maHS)
        var url = host + "/api/HosoSucKhoe/get-by-hs?maHS=" + currentMaHS;

        $http.get(url).then(function(res) {
            $scope.dsSucKhoe = res.data;
            // Lưu ý: API C# của bạn đã format NgayKham thành chuỗi "dd/MM/yyyy"
            // Nên bên HTML chỉ cần hiển thị {{sk.NgayKham}} là đẹp, không cần filter date nữa.
        }, function(err) {
            console.error("Lỗi tải hồ sơ sức khỏe:", err);
        });
    };

    // =============================================================
    // 6. GỌI API THÔNG BÁO
    // =============================================================
    $scope.loadThongBao = function() {
        // API C#: ThongBaoController
        $http.get(host + "/api/ThongBao/get-all").then(function(res) {
            if(res.data) {
                // Lọc tin nhắn phía Client: Lấy tin CHUNG hoặc tin RIÊNG của mã HS này
                $scope.dsThongBao = res.data.filter(x => 
                    x.MaHocSinh == null || 
                    x.MaHocSinh === "" || 
                    x.MaHocSinh == currentMaHS
                );
            }
        }, function(err) {
            console.error("Lỗi tải thông báo:", err);
        });
    };

    // Tự động tải thông tin cá nhân khi vào trang
    $scope.loadThongTin();
});