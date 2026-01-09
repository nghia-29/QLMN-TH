var app = angular.module('UngDungQuanLy', []);

app.controller('TKBController', function($scope, $http) {
    
    // --- CẤU HÌNH API ---
    var currentUrl = "http://localhost:6001"; 
    var apiBase = currentUrl + "/api/ThoiKhoaBieu";

    $scope.dsTKB = [];
    $scope.tkb = {};
    $scope.cheDoSua = false;

    // 1. LOAD DANH SÁCH (Gọi API get-all)
    $scope.layDanhSach = function() {
        $http.get(apiBase + "/get-all")
        .then(function(res) {
            $scope.dsTKB = res.data;
        }, function(err) { 
            console.error("Lỗi load danh sách:", err); 
        });
    };
    $scope.layDanhSach();

    // 2. CHỌN DÒNG (Đưa dữ liệu lên Form)
    $scope.chonDong = function(item) {
        $scope.tkb = {
            MaTkb:       item.maTkb,       // Map đúng với Model C# (Property viết hoa)
            MaLop:       item.maLop,
            MaMonHoc:    item.maMonHoc,
            TenGiaoVien: item.tenGiaoVien,
            Thu:         item.thu,
            TietHoc:     item.tietHoc,
            MaPhanCong:  item.maPhanCong
        };
        $scope.cheDoSua = true; // Hiện nút Sửa/Xóa
    };

    // 3. THÊM MỚI
    $scope.themMoi = function() {
        if (!$scope.tkb.MaTkb || !$scope.tkb.MaLop) {
            alert("Vui lòng nhập Mã TKB và Mã Lớp!");
            return;
        }

        $http.post(apiBase + "/create", $scope.tkb)
        .then(function(res) {
            alert("✅ Thêm thành công: " + res.data);
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            alert("❌ Lỗi: " + (err.data || "Lỗi kết nối Server")); 
        });
    };

    // 4. CẬP NHẬT
    $scope.capNhat = function() {
        $http.post(apiBase + "/update", $scope.tkb)
        .then(function(res) {
            alert("✅ Cập nhật thành công: " + res.data);
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            alert("❌ Lỗi: " + (err.data || "Lỗi kết nối Server")); 
        });
    };

    // 5. XÓA
    $scope.xoa = function() {
        if (!confirm("Bạn có chắc muốn xóa TKB này?")) return;

        // API của bạn xóa bằng POST và query string ?id=...
        $http.post(apiBase + "/delete?id=" + $scope.tkb.MaTkb)
        .then(function(res) {
            alert("✅ Đã xóa thành công: " + res.data);
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            alert("❌ Lỗi: " + (err.data || "Lỗi kết nối Server")); 
        });
    };

    // 6. LÀM MỚI FORM
    $scope.lamMoi = function() {
        $scope.tkb = {};
        $scope.cheDoSua = false;
    };
});