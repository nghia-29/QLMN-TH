var app = angular.module('UngDungQuanLy', []);

app.controller('TaiKhoanController', function($scope, $http) {
    
    // Đổi lại link API cho đúng Controller C# của bạn
    var currentUrl = "http://localhost:6001"; 
    var apiBase = currentUrl + "/api/TaiKhoan";

    $scope.dsTaiKhoan = [];
    $scope.tk = {};
    $scope.cheDoSua = false;

    // 1. LOAD DANH SÁCH
    $scope.layDanhSach = function() {
        $http.get(apiBase + "/get-all")
        .then(function(res) {
            $scope.dsTaiKhoan = res.data;
        }, function(err) { 
            console.error("Lỗi:", err);
        });
    };
    $scope.layDanhSach();

    // 2. CHỌN DÒNG (Binding dữ liệu từ bảng lên form)
    $scope.chonDong = function(item) {
        $scope.tk = {
            IDTaiKhoan:  item.idTaiKhoan,   // Chú ý: API C# thường trả về chữ cái đầu thường (camelCase)
            TenTaiKhoan: item.tenTaiKhoan,
            MatKhau:     item.matKhau,
            MaLoai:      item.maLoai,
            TenLoai:     item.tenLoai       // Trường này chỉ để hiển thị, không gửi đi khi sửa
        };
        $scope.cheDoSua = true;
    };

    // 3. THÊM MỚI (Gọi sp_TaiKhoan_Insert)
    $scope.themMoi = function() {
        if (!$scope.tk.IDTaiKhoan || !$scope.tk.TenTaiKhoan || !$scope.tk.MatKhau) {
            alert("Vui lòng nhập đủ ID, Tên đăng nhập và Mật khẩu!"); 
            return;
        }

        $http.post(apiBase + "/create", $scope.tk)
        .then(function(res) {
            alert("✅ " + res.data); // Hiện thông báo từ SQL
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            alert("❌ Lỗi: " + (err.data || "Lỗi Server")); 
        });
    };

    // 4. CẬP NHẬT (Gọi sp_TaiKhoan_Update)
    $scope.capNhat = function() {
        // SQL Update chỉ nhận: IDTaiKhoan, MatKhau, MaLoai
        $http.post(apiBase + "/update", $scope.tk)
        .then(function(res) {
            alert("✅ " + res.data);
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            alert("❌ Lỗi: " + (err.data || "Lỗi Server")); 
        });
    };

    // 5. XÓA (Gọi sp_TaiKhoan_Delete)
    $scope.xoa = function() {
        if (!confirm("Bạn có chắc muốn xóa tài khoản này?")) return;
        
        // Gửi ID qua query string (hoặc body tùy controller của bạn)
        $http.post(apiBase + "/delete?id=" + $scope.tk.IDTaiKhoan)
        .then(function(res) {
            alert("✅ " + res.data);
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            alert("❌ Lỗi: " + (err.data || "Lỗi Server")); 
        });
    };

    // 6. LÀM MỚI
    $scope.lamMoi = function() {
        $scope.tk = {};
        $scope.cheDoSua = false;
    };
});