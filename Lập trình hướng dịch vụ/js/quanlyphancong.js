var app = angular.module('UngDungQuanLy', []);

app.controller('PhanCongController', function($scope, $http) {
    
    // --- CẤU HÌNH API ---
    var currentUrl = "http://localhost:6001"; 
    var apiBase = currentUrl + "/api/PhanCong";

    $scope.dsPhanCong = [];
    $scope.pc = {};
    $scope.cheDoSua = false;

    // 1. LOAD DANH SÁCH
    $scope.layDanhSach = function() {
        $http.get(apiBase + "/get-all")
        .then(function(res) {
            $scope.dsPhanCong = res.data;
        }, function(err) { 
            console.error("Lỗi load danh sách:", err); 
        });
    };
    $scope.layDanhSach();

    // 2. CHỌN DÒNG
    $scope.chonDong = function(item) {
        $scope.pc = {
            MaPhanCong: item.maPhanCong,
            MaLop:      item.maLop,
            MaMonHoc:   item.maMonHoc,
            NamHoc:     item.namHoc,
            HocKy:      item.hocKy,
            MaGiaoVien: item.maGiaoVien,
            MaHocSinh:  item.maHocSinh
        };
        $scope.cheDoSua = true; // Hiện các nút Sửa/Xóa
    };

    // 3. THÊM MỚI
    $scope.themMoi = function() {
        if (!$scope.pc.MaPhanCong) { 
            alert("Vui lòng nhập Mã phân công!"); 
            return; 
        }
        
        $http.post(apiBase + "/create", $scope.pc)
        .then(function(res) {
            alert("✅ Thêm mới thành công!");
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            xyLyLoi(err);
        });
    };

    // 4. CẬP NHẬT
    $scope.capNhat = function() {
        $http.post(apiBase + "/update", $scope.pc)
        .then(function(res) {
            alert("✅ Cập nhật thành công!");
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            xyLyLoi(err);
        });
    };

    // 5. XÓA (Đã xử lý lỗi khóa ngoại FK)
    $scope.xoa = function() {
        if (!confirm("Bạn có chắc muốn xóa phân công này không?")) return;

        // Gọi API Delete
        $http.post(apiBase + "/delete/" + $scope.pc.MaPhanCong)
        .then(function(res) {
            alert("✅ Đã xóa thành công!");
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            // Xử lý riêng cho lỗi xóa
            var loi = err.data || "";
            if (typeof loi === 'string' && (loi.includes("REFERENCE constraint") || loi.includes("FK_"))) {
                alert("❌ KHÔNG THỂ XÓA!\n\nLý do: Phân công này đang được dùng trong Thời Khóa Biểu.\n\nGiải pháp: Hãy xóa dữ liệu bên Quản lý TKB trước.");
            } else {
                xyLyLoi(err);
            }
        });
    };

    // 6. LÀM MỚI FORM
    $scope.lamMoi = function() {
        $scope.pc = {};
        $scope.cheDoSua = false;
    };

    // HÀM XỬ LÝ LỖI CHUNG
    function xyLyLoi(err) {
        var thongBao = err.data;
        if (!thongBao) thongBao = "Lỗi kết nối Server (" + err.status + ")";
        alert("❌ Lỗi: " + thongBao);
        console.error(err);
    }
});